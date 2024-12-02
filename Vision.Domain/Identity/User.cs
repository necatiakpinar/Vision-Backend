using Microsoft.AspNetCore.Identity;
using Vision.Domain.Common;

namespace Vision.Domain.Identity;

public class User : IdentityUser<string>, IEntity
{
    public string NameSurname { get; set; }
    public List<string> Roles { get; set; } = new List<string>();

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}