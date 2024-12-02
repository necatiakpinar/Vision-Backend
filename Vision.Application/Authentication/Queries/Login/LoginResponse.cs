using Vision.Application.Authentication.Common.Interfaces;
using Vision.Application.Common;
using Vision.Domain.Identity;

namespace Vision.Application.Authentication.Queries.Login;

public class LoginResponse : IResponse
{
    public User User { get; set; }
    public string Token { get; set; }

}