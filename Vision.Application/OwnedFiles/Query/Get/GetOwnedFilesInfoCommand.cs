using MediatR;
using Vision.Application.Authentication.Common;
using Vision.Application.SharedFiles.Command.Add;

namespace Vision.Application.OwnedFiles.Query.Get;

public class GetOwnedFilesInfoCommand :  IRequest<GenericResponse<GetOwnedFilesInfoResponse>>
{
    public string OwnerId { get; set; }
}