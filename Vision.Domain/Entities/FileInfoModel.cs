using Vision.Domain.Common;

namespace Vision.Domain.Entities;

public class FileInfoModel : BaseEntity
{
    public string FileName { get; set; }
    public string FileExtension { get; set; }
    public string OwnerUserName { get; set; }
}