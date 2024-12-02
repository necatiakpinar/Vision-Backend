using Vision.Application.Authentication.Common.Interfaces;

namespace Vision.Application.Image.ProfileImage.Queries.Get;

public class GetProfileImageResponse : IResponse
{
    public string Content { get; set; }
}