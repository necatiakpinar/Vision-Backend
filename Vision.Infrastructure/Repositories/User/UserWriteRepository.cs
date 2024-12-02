using MongoDB.Driver;
using Vision.Application.Repositories.User;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.User;

public class UserWriteRepository : WriteRepository<Domain.Identity.User>, IUserWriteRepository
{
    public UserWriteRepository(VisionDbContext context) : base(context)
    {
    }
    public async Task AddToRoleAsync(Domain.Identity.User user, string roleName, CancellationToken cancellationToken = default)
    {
        if (user.Roles == null)
        {
            user.Roles = new List<string>();
        }

        if (!user.Roles.Contains(roleName))
        {
            user.Roles.Add(roleName);
            context.Set<Domain.Identity.User>().Update(user);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
    
    public async Task RemoveFromRoleAsync(Domain.Identity.User user, string roleName, CancellationToken cancellationToken = default)
    {
        if (user.Roles == null)
        {
            return;
        }

        if (user.Roles.Contains(roleName))
        {
            user.Roles.Remove(roleName);
            context.Set<Domain.Identity.User>().Update(user);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}