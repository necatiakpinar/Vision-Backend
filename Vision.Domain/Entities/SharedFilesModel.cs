using Vision.Domain.Common;

namespace Vision.Domain.Entities;

public class SharedFilesModel : BaseEntity
{
    public string UserId { get; set; }
    public List<FileInfoModel> Files { get; set; }
}