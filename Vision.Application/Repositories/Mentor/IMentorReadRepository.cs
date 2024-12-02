using Vision.Domain.Entities;

namespace Vision.Application.Repositories.Mentor;

public interface IMentorReadRepository : IReadRepository<MentorModel>
{
    public Task<MentorModel?> GetMentorByUserId(string userId);
}