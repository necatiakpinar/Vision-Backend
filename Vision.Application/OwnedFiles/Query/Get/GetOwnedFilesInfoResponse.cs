using Vision.Application.Authentication.Common.Interfaces;
using Vision.Domain.Entities;

namespace Vision.Application.OwnedFiles.Query.Get;

public class GetOwnedFilesInfoResponse : IResponse
{
    public List<FileInfoModel> Files { get; set; }
}