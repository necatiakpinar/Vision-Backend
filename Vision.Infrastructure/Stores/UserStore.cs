using Microsoft.AspNetCore.Identity;
using Vision.Application.Repositories.User;
using Vision.Domain.Identity;

namespace Vision.Infrastructure.Stores;

public class UserStore<TUser> : IUserPasswordStore<TUser>, IUserEmailStore<TUser>, IUserRoleStore<TUser> where TUser : User
{
    private readonly IUserWriteRepository _userWriteRepository;
    private readonly IUserReadRepository _userReadRepository;

    public UserStore(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository)
    {
        _userReadRepository = userReadRepository;
        _userWriteRepository = userWriteRepository;
    }

    public async Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken)
    {
        var id = user.Id.ToString();
        var foundUser = await _userReadRepository.GetByIdAsync(id, true, cancellationToken: cancellationToken);

        return foundUser.Id.ToString();
    }

    public async Task<string?> GetUserNameAsync(TUser user, CancellationToken cancellationToken)
    {
        return await Task.FromResult(user.UserName);
    }

    public async Task SetUserNameAsync(TUser user, string? userName, CancellationToken cancellationToken)
    {
        user.UserName = userName;
        await Task.CompletedTask;
    }

    public async Task<string?> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken)
    {
        return await Task.FromResult(user.NormalizedUserName);
    }

    public async Task SetNormalizedUserNameAsync(TUser user, string? normalizedName, CancellationToken cancellationToken)
    {
        user.NormalizedUserName = normalizedName;
        await Task.CompletedTask;
    }

    public async Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken)
    {
        await _userWriteRepository.AddAsync(user, cancellationToken);
        await _userWriteRepository.SaveChangesAsync();
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken)
    {
        _userWriteRepository.Update(user);
        await _userWriteRepository.SaveChangesAsync();
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken)
    {
        await _userWriteRepository.RemoveAsync(user.Id.ToString(), cancellationToken);
        await _userWriteRepository.SaveChangesAsync();
        return IdentityResult.Success;
    }

    public async Task<TUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userReadRepository.GetByIdAsync(userId, cancellationToken: cancellationToken);

        return (TUser)user;
    }

    public async Task<TUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        var user = await _userReadRepository.GetByIdAsync(normalizedUserName, cancellationToken: cancellationToken);

        return (TUser)user;
    }

    public void Dispose()
    {

    }

    public async Task SetPasswordHashAsync(TUser user, string? passwordHash, CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash;
        await Task.CompletedTask;
    }

    public async Task<string?> GetPasswordHashAsync(TUser user, CancellationToken cancellationToken)
    {
        return await Task.FromResult(user.PasswordHash);
    }

    public async Task<bool> HasPasswordAsync(TUser user, CancellationToken cancellationToken)
    {
        return await Task.FromResult(user.PasswordHash != null);
    }

    public async Task SetEmailAsync(TUser user, string? email, CancellationToken cancellationToken)
    {
        user.Email = email;
        await Task.CompletedTask;
    }

    public async Task<string?> GetEmailAsync(TUser user, CancellationToken cancellationToken)
    {
        return await Task.FromResult(user.Email);
    }

    public async Task<bool> GetEmailConfirmedAsync(TUser user, CancellationToken cancellationToken)
    {
        return await Task.FromResult(user.EmailConfirmed);
    }

    public async Task SetEmailConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
    {
        user.EmailConfirmed = confirmed;
        await Task.CompletedTask;
    }

    public async Task<TUser?> FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _userReadRepository.GetByEmailAsync(email.ToUpperInvariant(), true, cancellationToken: cancellationToken);
        if (user == null)
        {
            return null;
        }

        return (TUser)user;
    }

    public async Task<string?> GetNormalizedEmailAsync(TUser user, CancellationToken cancellationToken)
    {
        return await Task.FromResult(user.NormalizedEmail);
    }

    public async Task SetNormalizedEmailAsync(TUser user, string? normalizedEmail, CancellationToken cancellationToken)
    {
        user.NormalizedEmail = normalizedEmail;
        await Task.CompletedTask;
    }
    public async Task AddToRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
    {
        await _userWriteRepository.AddToRoleAsync(user, roleName, cancellationToken);
    }

    public async Task RemoveFromRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
    {
        await _userWriteRepository.RemoveFromRoleAsync(user, roleName, cancellationToken);
    }

    public async Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken)
    {
        return await Task.FromResult(user.Roles ?? new List<string>());
    }


    public async Task<bool> IsInRoleAsync(TUser user, string roleName, CancellationToken cancellationToken)
    {
        return await Task.FromResult(user.Roles != null && user.Roles.Contains(roleName));
    }

    public async Task<IList<TUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        var users = await _userReadRepository.GetUsersInRoleAsync(roleName, cancellationToken);
        return users.Cast<TUser>().ToList();
    }
}