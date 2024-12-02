using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vision.Application.Authentication.Common;
using Vision.Application.Common;
using Vision.Application.Repositories.Leaderboard;
using Vision.Application.Repositories.UserProfile;
using Vision.Domain.Entities;
using Vision.Domain.Identity;

namespace Vision.Application.Authentication.Commands.Delete;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, GenericResponse<DeleteUserResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserProfileReadRepository _userProfileReadRepository;
    private readonly IUserProfileWriteRepository _userProfileWriteRepository;
    private readonly ILeaderboardReadRepository _leaderboardReadRepository;
    private readonly ILeaderboardWriteRepository _leaderboardWriteRepository;

    public DeleteUserHandler(
        UserManager<User> userManager,
        IUserProfileWriteRepository userProfileWriteRepository,
        ILeaderboardWriteRepository leaderboardWriteRepository,
        IUserProfileReadRepository userProfileReadRepository,
        ILeaderboardReadRepository leaderboardReadRepository)
    {
        _userManager = userManager;
        _userProfileWriteRepository = userProfileWriteRepository;
        _leaderboardWriteRepository = leaderboardWriteRepository;
        _userProfileReadRepository = userProfileReadRepository;
        _leaderboardReadRepository = leaderboardReadRepository;
    }

    public async Task<GenericResponse<DeleteUserResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        // Kullanıcıyı bul ve sil
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
        {
            return new GenericResponse<DeleteUserResponse>()
            {
                WarningResult = new WarningResult()
                {
                    Title = "User not found",
                    Message = "User not found"
                }
            };
        }


        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            return new GenericResponse<DeleteUserResponse>()
            {
                WarningResult = new WarningResult()
                {
                    Title = "Error deleting user",
                    Message = "Error deleting user"
                }
            };
        }

        var userProfile = await _userProfileReadRepository.GetByIdAsync(request.UserId.ToString(), cancellationToken: cancellationToken);
        if (userProfile != null)
        {
            _userProfileWriteRepository.Remove(userProfile);
            await _userProfileWriteRepository.SaveChangesAsync();
        }

        var leaderboardEntries = await _leaderboardReadRepository.GetWhere(data => data.UserId == request.UserId, tracking: false)
            .ToListAsync(cancellationToken);
        if (leaderboardEntries.Any())
        {
            _leaderboardWriteRepository.RemoveRange(leaderboardEntries);
            await _leaderboardWriteRepository.SaveChangesAsync();
        }

        return new GenericResponse<DeleteUserResponse>()
        {
            Data = new DeleteUserResponse
            {
                Succeeded = true
            }
        };
    }
}