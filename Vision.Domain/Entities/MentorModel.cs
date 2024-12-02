using Vision.Domain.Common;

namespace Vision.Domain.Entities;

public class MentorModel : BaseEntity
{
    public string MentorId { get; set; }
    public List<FollowerModel> Followers { get; set; } = new(); 
    
}