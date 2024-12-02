using MediatR;
using Microsoft.EntityFrameworkCore;
using Vision.Application.Authentication.Common;
using Vision.Application.Common;
using Vision.Application.Repositories.ProfileImage;


namespace Vision.Application.Image.ProfileImage.Queries.Get;

public class GetProfileImageHandler : IRequestHandler<GetProfileImageQuery, GenericResponse<GetProfileImageResponse>>
{
    private readonly IProfileImageReadRepository _imageReadRepository;

    public GetProfileImageHandler(IProfileImageReadRepository imageReadRepository)
    {
        _imageReadRepository = imageReadRepository;
    }

    public async Task<GenericResponse<GetProfileImageResponse>> Handle(GetProfileImageQuery request, CancellationToken cancellationToken)
    {
        var imageModel = await _imageReadRepository.Table
            .FirstOrDefaultAsync(x => x.Id == request.ImageId, cancellationToken);

        if (imageModel == null)
        {
            return new GenericResponse<GetProfileImageResponse>
            {
                Data = null,
                WarningResult = new WarningResult
                {
                    Title = "Image not found",
                    Message = "Image not found"
                }
            };
        }

        var response = new GetProfileImageResponse
        {
            Content = imageModel.Base64Content
        };

        return new GenericResponse<GetProfileImageResponse>
        {
            Data = response
        };
    }
}