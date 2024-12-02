using MediatR;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using Vision.Application.Authentication.Common;
using Vision.Application.Common;
using Vision.Application.Repositories.Leaderboard;
using Vision.Application.Repositories.User;
using Vision.Application.Repositories.UserProfile;
using Vision.Domain.Entities;
using Vision.Domain.Identity;

namespace Vision.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, GenericResponse<RegisterResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserProfileWriteRepository _userProfileWriteRepository;
    private readonly ILeaderboardWriteRepository _leaderboardWriteRepository;
    public RegisterCommandHandler(
        UserManager<User> userManager,
        IUserProfileWriteRepository userProfileWriteRepository,
        ILeaderboardWriteRepository leaderboardWriteRepository)
    {
        _userManager = userManager;
        _userProfileWriteRepository = userProfileWriteRepository;
        _leaderboardWriteRepository = leaderboardWriteRepository;
    }
    public async Task<GenericResponse<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var newUser = new User
        {
            Id = Guid.NewGuid().ToString(),
            NameSurname = request.FirstName + " " + request.LastName,
            UserName = request.UserName,
            Email = request.Email,
        };

        if (_userManager.FindByEmailAsync(request.Email).Result != null)
        {
            var authResponse = new GenericResponse<RegisterResponse>
            {
                Data = new RegisterResponse
                {
                    Succeeded = false
                },
                WarningResult = new WarningResult
                {
                    Title = "Email Exists",
                    Message = "Email already exists"
                }
            };
            return authResponse;
        }

        var result = await _userManager.CreateAsync(newUser, request.Password);
        var roleResult = await _userManager.AddToRoleAsync(newUser, "Member");

        if (!result.Succeeded || !roleResult.Succeeded)
        {
            var authResponse = new GenericResponse<RegisterResponse>
            {
                Data = new RegisterResponse
                {
                    Succeeded = false
                },
                WarningResult = new WarningResult
                {
                    Title = result.Errors.First().Code,
                    Message = result.Errors.First().Description
                }
            };
            return authResponse;
        }

        var userProfile = new UserProfile()
        {
            Id = newUser.Id,
            GivenName = request.FirstName,
            Surname = newUser.NameSurname,
            Email = newUser.Email
        };

        await _userProfileWriteRepository.AddAsync(userProfile);
        await _userProfileWriteRepository.SaveChangesAsync();
        
        var leaderboardId = Guid.NewGuid().ToString();
        var leaderboardEntry = new Domain.Entities.Leaderboard
        {
            Id = leaderboardId,
            UserId = newUser.Id,
            UserName = newUser.UserName,
            Score = 0
        };

        await _leaderboardWriteRepository.AddAsync(leaderboardEntry);
        await _leaderboardWriteRepository.SaveChangesAsync();

        var successfulResponse = new RegisterResponse
        {
            Succeeded = true
        };

        var authResult = new GenericResponse<RegisterResponse>
        {
            Data = successfulResponse
        };

        return authResult;

    }

}