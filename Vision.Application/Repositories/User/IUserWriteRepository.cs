namespace Vision.Application.Repositories.User;

public interface IUserWriteRepository : IWriteRepository<Domain.Identity.User>
{
    Task AddToRoleAsync(Domain.Identity.User user, string roleName, CancellationToken cancellationToken = default);
    Task RemoveFromRoleAsync(Domain.Identity.User user, string roleName, CancellationToken cancellationToken = default);

}