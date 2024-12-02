using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Vision.Application.Repositories.Role;
using Vision.Domain.Identity;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.Role;

public class RoleReadRepository : ReadRepository<AppRole>, IRoleReadRepository
{
    public RoleReadRepository(VisionDbContext context) : base(context)
    {
    }
    
    public async Task<AppRole?> GetAsync(Expression<Func<AppRole, bool>> predicate)
    {
        return await Table.FirstOrDefaultAsync(predicate);
    }
}