using System.Linq.Expressions;
using Vision.Domain.Identity;

namespace Vision.Application.Repositories.Role;

public interface IRoleReadRepository : IReadRepository<AppRole>
{
    Task<AppRole?> GetAsync(Expression<Func<AppRole, bool>> predicate);
}