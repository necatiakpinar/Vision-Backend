
namespace Vision.Application.Repositories.User;

public interface IUserReadRepository : IReadRepository<Domain.Identity.User>
{
    public Task<Domain.Identity.User?> GetByNameAsync(string normalizedUserName, bool tracking = true, CancellationToken cancellationToken = default);
    public Task<Domain.Identity.User?> GetByEmailAsync(string normalizedEmail, bool tracking = true, CancellationToken cancellationToken = default);
    public Task<IList<Domain.Identity.User?>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default);

}