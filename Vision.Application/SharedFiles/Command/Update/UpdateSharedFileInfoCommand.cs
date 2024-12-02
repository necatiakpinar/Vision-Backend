using MediatR;
using Vision.Application.Authentication.Common;

namespace Vision.Application.SharedFiles.Command.Update;

public class UpdateSharedFileInfoCommand : IRequest<GenericResponse<UpdateSharedFileInfoResponse>>
{
    public string UserId { get; set; }
    public string FileId { get; set; }
}