using Vision.Application.Repositories.Role;
using Vision.Domain.Identity;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.Role;

public class RoleWriteRepository : WriteRepository<AppRole>, IRoleWriteRepository
{
    public RoleWriteRepository(VisionDbContext context) : base(context)
    {
    }
}
