using MediatR;
using Vision.Application.Authentication.Common;
using Vision.Application.Common;
using Vision.Application.Repositories.ProfileImage;
using Vision.Application.Services;
using Vision.Domain.Entities;

namespace Vision.Application.Image.ProfileImage.Commands.Upload;

public class UploadProfileImageHandler : IRequestHandler<UploadProfileImageCommand, GenericResponse<UploadProfileImageResponse>>
{

    private readonly IProfileImageWriteRepository _profileImageWriteRepository;
    public UploadProfileImageHandler(IProfileImageWriteRepository profileImageWriteRepository)
    {
        _profileImageWriteRepository = profileImageWriteRepository;
    }

    public async Task<GenericResponse<UploadProfileImageResponse>> Handle(UploadProfileImageCommand request, CancellationToken cancellationToken)
    {
        if (request.File == null || request.File.Length == 0)
        {
            var authResult = new GenericResponse<UploadProfileImageResponse>
            {
                Data = new UploadProfileImageResponse
                {
                    Succeeded = false
                },
                WarningResult = new WarningResult
                {
                    Title = "No file uploaded",
                    Message = "No file uploaded"
                }
            };

            return authResult;
        }

        using (var memoryStream = new MemoryStream())
        {
            await request.File.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            var base64Image = Convert.ToBase64String(fileBytes);

            var fileName = request.File.FileName;
            var fileExtension = Path.GetExtension(fileName);


            var imageModel = new ImageModel
            {
                Id = Guid.NewGuid().ToString(),
                FileName = fileName,
                FileExtension = fileExtension,
                Base64Content = base64Image
            };

            await _profileImageWriteRepository.AddAsync(imageModel);
            await _profileImageWriteRepository.SaveChangesAsync();

            var succesFulResponse = new UploadProfileImageResponse
            {
                Succeeded = true
            };
            var authResult = new GenericResponse<UploadProfileImageResponse>
            {
                Data = succesFulResponse
            };

            return authResult;
        }
    }
}