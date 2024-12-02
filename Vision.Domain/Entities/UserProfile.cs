using Vision.Domain.Common;

namespace Vision.Domain.Entities;

public class UserProfile : BaseEntity
{
    public string GivenName { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
}