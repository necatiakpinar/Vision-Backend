using MediatR;
using Vision.Application.Authentication.Common;

namespace Vision.Application.SharedFiles.Command.AddMultiple;

public class AddMultipleSharedFilesInfoCommand : IRequest<GenericResponse<AddMultipleSharedFilesInfoResponse>>
{
    public List<string> UserIds { get; set; }
    public string FileId { get; set; }
}