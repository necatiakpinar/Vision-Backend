using Vision.Domain.Identity;

namespace Vision.Application.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(User userProfile, IList<string> roles);
}