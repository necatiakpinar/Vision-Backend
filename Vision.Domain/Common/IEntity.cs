using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Vision.Domain.Common;

public interface IEntity
{
    string Id { get; set; }
    DateTime CreatedDate { get; set; }
    DateTime UpdatedDate { get; set; }
}