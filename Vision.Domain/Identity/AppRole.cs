using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using Vision.Domain.Common;

namespace Vision.Domain.Identity;

public class AppRole : IdentityRole<string>, IEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}