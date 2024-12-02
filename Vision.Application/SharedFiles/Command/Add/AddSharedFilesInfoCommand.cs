using MediatR;
using Vision.Application.Authentication.Common;


namespace Vision.Application.SharedFiles.Command.Add;

public class AddSharedFilesInfoCommand : IRequest<GenericResponse<AddSharedFilesInfoResponse>>
{
    public string UserId { get; set; }
    public string FileId { get; set; }
}