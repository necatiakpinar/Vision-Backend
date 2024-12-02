using Vision.Domain.Common;

namespace Vision.Domain.Entities;

public class OwnedFilesModel : BaseEntity
{
    public string OwnerId { get; set; }
    public List<FileInfoModel> Files { get; set; }
}