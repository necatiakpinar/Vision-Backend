using Microsoft.EntityFrameworkCore;
using Vision.Application.Repositories.User;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.User;

public class UserReadRepository : ReadRepository<Domain.Identity.User>, IUserReadRepository
{
    public UserReadRepository(VisionDbContext context) : base(context)
    {
    }
    
    public async Task<Domain.Identity.User?> GetByNameAsync(string normalizedUserName, bool tracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<Domain.Identity.User?> query = Table.AsQueryable();
        if (!tracking)
            query = Table.AsNoTracking();

        return await query.FirstOrDefaultAsync(data => data != null && data.UserName == normalizedUserName,
            cancellationToken);
    }
    
    public async Task<Domain.Identity.User?> GetByEmailAsync(string normalizedEmail, bool tracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<Domain.Identity.User?> query = Table.AsQueryable();
        if (!tracking)
            query = Table.AsNoTracking();

        return await query.FirstOrDefaultAsync(data => data != null && data.NormalizedEmail == normalizedEmail, 
            cancellationToken);
    }
  
    public async Task<IList<Domain.Identity.User?>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default)
    {
        IQueryable<Domain.Identity.User> query = context.Users.AsQueryable(); 
        return await query
            .Where(user => user.Roles.Contains(roleName))
            .ToListAsync(cancellationToken);
    }
}