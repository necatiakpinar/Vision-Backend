using Vision.Application.Authentication.Common.Interfaces;
using Vision.Domain.Entities;

namespace Vision.Application.SharedFiles.Query.Get;

public class GetSharedFileInfoResponse : IResponse
{
    public List<FileInfoModel> Files { get; set; }
    
}