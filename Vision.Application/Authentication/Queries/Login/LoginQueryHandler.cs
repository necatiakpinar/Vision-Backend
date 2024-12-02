using MediatR;
using Microsoft.AspNetCore.Identity;
using Vision.Application.Authentication.Common;
using Vision.Application.Common;
using Vision.Application.Services;
using Vision.Domain.Identity;

namespace Vision.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, GenericResponse<LoginResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;

    }
    public async Task<GenericResponse<LoginResponse>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(query.Email);

        if (user == null)
        {
            return new GenericResponse<LoginResponse>
            {
                Data = null,
                WarningResult = new WarningResult
                {
                    Title = "User not found",
                    Message = "User not found"
                }
            };
        }

        var checkPasswordAsync = await _userManager.CheckPasswordAsync(user, query.Password);

        if (!checkPasswordAsync)
        {
            return new GenericResponse<LoginResponse>
            {
                Data = null,
                WarningResult = new WarningResult
                {
                    Title = "Password is incorrect",
                    Message = "Password is incorrect"
                }
            };
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token =_jwtTokenGenerator.GenerateToken(user, roles);
        
        var roleResult = await _userManager.IsInRoleAsync(user, roles[0]);
        Console.WriteLine(roleResult);
        
        var loginResponse = new LoginResponse
        {
            User = user,
            Token = token
        };

        return new GenericResponse<LoginResponse>
        {
            Data = loginResponse
        };


    }
}