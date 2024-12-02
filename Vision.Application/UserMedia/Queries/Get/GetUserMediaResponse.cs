using Vision.Application.Authentication.Common.Interfaces;
using Vision.Domain.Entities;

namespace Vision.Application.UserMedia.Queries.Get;

public class GetUserMediaResponse : IResponse
{
    public List<FileModel> MediaFiles { get; set; }
}