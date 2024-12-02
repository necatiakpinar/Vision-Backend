using MediatR;
using Vision.Application.Authentication.Common;

namespace Vision.Application.SharedFiles.Query.Get;

public class GetSharedFileInfoQuery : IRequest<GenericResponse<GetSharedFileInfoResponse>>
{
    public string UserId { get; set; }
}