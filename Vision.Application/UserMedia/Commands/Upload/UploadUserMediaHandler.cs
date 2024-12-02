using MediatR;
using Vision.Application.Authentication.Common;
using Vision.Application.Common;
using Vision.Application.Repositories.OwnedFiles;
using Vision.Application.Repositories.UserMedia;
using Vision.Domain.Entities;

namespace Vision.Application.UserMedia.Commands.Upload;

public class UploadUserMediaHandler : IRequestHandler<UploadUserMediaCommand, GenericResponse<UploadUserMediaResponse>>
{
    private readonly IUserMediaWriteRepository _userMediaWriteRepository;
    private readonly IOwnedFilesWriteRepository _ownedFilesWriteRepository;
    private readonly IOwnedFilesReadRepository _ownedFilesReadRepository;

    public UploadUserMediaHandler(IUserMediaWriteRepository userMediaWriteRepository, IOwnedFilesWriteRepository ownedFilesWriteRepository, IOwnedFilesReadRepository ownedFilesReadRepository)
    {
        _userMediaWriteRepository = userMediaWriteRepository;
        _ownedFilesWriteRepository = ownedFilesWriteRepository;
        _ownedFilesReadRepository = ownedFilesReadRepository;
    }

    public async Task<GenericResponse<UploadUserMediaResponse>> Handle(UploadUserMediaCommand command,
        CancellationToken cancellationToken)
    {
        if (command.File == null || command.File.Length == 0)
        {
            var authResult = new GenericResponse<UploadUserMediaResponse>
            {
                Data = new UploadUserMediaResponse
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
        
        if (command.UserId == null)
        {
            var authResult = new GenericResponse<UploadUserMediaResponse>
            {
                Data = new UploadUserMediaResponse
                {
                    Succeeded = false
                },
                WarningResult = new WarningResult
                {
                    Title = "User not found",
                    Message = "User not found"
                }
            };

            return authResult;
        }

        using (var memoryStream = new MemoryStream())
        {
            await command.File.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            var base64File = Convert.ToBase64String(fileBytes);

            var fileName = command.File.FileName;
            var fileExtension = Path.GetExtension(fileName);

            var fileModel = new FileModel
            {
                Id = Guid.NewGuid().ToString(),
                OwnerId = command.UserId,
                OwnerUserName = command.UserName,
                FileName = fileName,
                FileExtension = fileExtension,
                Content = base64File,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _userMediaWriteRepository.AddAsync(fileModel);
            await _userMediaWriteRepository.SaveChangesAsync();
            
            await AddOrUpdateOwnedFiles(command, fileModel);

            var successfulResponse = new UploadUserMediaResponse()
            {
                Succeeded = true
            };
            var authResult = new GenericResponse<UploadUserMediaResponse>
            {
                Data = successfulResponse
            };

            return authResult;
        }
    }

    private async Task AddOrUpdateOwnedFiles(UploadUserMediaCommand command, FileModel fileModel)
    {
        var fileInfoModel = new FileInfoModel()
        {
            Id = fileModel.Id,
            OwnerUserName = fileModel.OwnerUserName,
            FileName = fileModel.FileName,
            FileExtension = fileModel.FileExtension,
            CreatedDate = fileModel.CreatedDate,
            UpdatedDate = fileModel.UpdatedDate
        };

        var ownedFile = await _ownedFilesReadRepository.GetOwnedFilesByOwnerId(command.UserId)!;
        if (ownedFile != null)
        {
            ownedFile.Files.Add(fileInfoModel);
            _ownedFilesWriteRepository.Update(ownedFile);
            await _ownedFilesWriteRepository.SaveChangesAsync();
        }
        else
        {
            var newOwnedFile = new OwnedFilesModel()
            {
                Id = Guid.NewGuid().ToString(),
                OwnerId = command.UserId,
                Files = new List<FileInfoModel>() { fileInfoModel }
            };

            await _ownedFilesWriteRepository.AddAsync(newOwnedFile);
            await _ownedFilesWriteRepository.SaveChangesAsync();
        }
    }
}