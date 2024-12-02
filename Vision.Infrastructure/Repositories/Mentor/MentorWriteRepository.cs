using Vision.Application.Repositories.Mentor;
using Vision.Domain.Entities;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.Mentor;

public class MentorWriteRepository : WriteRepository<MentorModel>, IMentorWriteRepository
{
    public MentorWriteRepository(VisionDbContext context) : base(context)
    {
    }
}