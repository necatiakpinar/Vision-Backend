using MongoDB.Bson.Serialization.Attributes;
using Vision.Domain.Common;

namespace Vision.Domain.Entities;

public class ImageModel : BaseEntity
{
    public string FileName { get; set; }
    public string FileExtension { get; set; }
    public string Base64Content { get; set; }
}