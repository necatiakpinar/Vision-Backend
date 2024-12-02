using Microsoft.EntityFrameworkCore;
using Vision.Application.Repositories.Mentor;
using Vision.Domain.Entities;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.Mentor;

public class MentorReadRepository : ReadRepository<MentorModel>, IMentorReadRepository
{
    public MentorReadRepository(VisionDbContext context) : base(context)
    {
    }

    public Task<MentorModel?> GetMentorByUserId(string userId)
    {
        var query = Table.AsQueryable();
        
        return query.FirstOrDefaultAsync(data => data.MentorId == userId);
    }
}