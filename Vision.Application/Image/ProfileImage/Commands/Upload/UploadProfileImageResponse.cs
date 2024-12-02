using Vision.Application.Authentication.Common.Interfaces;

namespace Vision.Application.Image.ProfileImage.Commands.Upload;

public class UploadProfileImageResponse : IResponse
{
    public bool Succeeded { get; set; }
}