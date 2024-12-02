using Vision.Domain.Common;

namespace Vision.Domain.Entities;

public class FileModel : BaseEntity
{
    public string OwnerId { get; set; }
    public string OwnerUserName { get; set; }
    public string FileName { get; set; }
    public string FileExtension { get; set; }
    public string Content { get; set; }
}