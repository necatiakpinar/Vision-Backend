using Vision.Domain.Common;

namespace Vision.Domain.Entities;

public class Leaderboard : BaseEntity
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public int Score { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}