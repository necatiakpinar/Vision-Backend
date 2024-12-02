using Vision.Application.Authentication.Common.Interfaces;

namespace Vision.Application.UserMedia.Commands.Upload;

public class UploadUserMediaResponse : IResponse
{
    public bool Succeeded { get; set; }
}