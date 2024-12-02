using MediatR;
using Microsoft.AspNetCore.Http;
using Vision.Application.Authentication.Common;

namespace Vision.Application.Image.ProfileImage.Commands.Upload;

public class UploadProfileImageCommand : IRequest<GenericResponse<UploadProfileImageResponse>>
{
    public IFormFile File { get; set; }

}