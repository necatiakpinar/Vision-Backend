using MediatR;
using Microsoft.AspNetCore.Http;
using Vision.Application.Authentication.Common;

namespace Vision.Application.UserMedia.Commands.Upload;

public class UploadUserMediaCommand : IRequest<GenericResponse<UploadUserMediaResponse>>
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public IFormFile File { get; set; }
}
