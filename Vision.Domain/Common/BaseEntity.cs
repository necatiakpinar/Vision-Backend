using MongoDB.Bson;

namespace Vision.Domain.Common;

public class  BaseEntity : IEntity
{
    public string Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}