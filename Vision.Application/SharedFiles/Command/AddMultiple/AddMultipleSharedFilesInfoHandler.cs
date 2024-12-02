using MediatR;
using Microsoft.EntityFrameworkCore;
using Vision.Application.Authentication.Common;
using Vision.Application.Repositories.SharedFiles;
using Vision.Application.Repositories.UserMedia;
using Vision.Domain.Entities;

namespace Vision.Application.SharedFiles.Command.AddMultiple;

public class AddMultipleSharedFilesInfoHandler : IRequestHandler<AddMultipleSharedFilesInfoCommand, GenericResponse<AddMultipleSharedFilesInfoResponse>>
{
    public readonly ISharedFilesWriteRepository _sharedFilesWriteRepository;
    public readonly ISharedFilesReadRepository _sharedFilesReadRepository;
    public readonly IUserMediaReadRepository _userMediaReadRepository;

    public AddMultipleSharedFilesInfoHandler(ISharedFilesWriteRepository sharedFilesWriteRepository, ISharedFilesReadRepository sharedFilesReadRepository, IUserMediaReadRepository userMediaReadRepository)
    {
        _sharedFilesWriteRepository = sharedFilesWriteRepository;
        _sharedFilesReadRepository = sharedFilesReadRepository;
        _userMediaReadRepository = userMediaReadRepository;
    }

    public async Task<GenericResponse<AddMultipleSharedFilesInfoResponse>> Handle(
        AddMultipleSharedFilesInfoCommand command, CancellationToken cancellationToken)
    {
        var userMediaFile = await _userMediaReadRepository.Table.FirstOrDefaultAsync(x => x.Id == command.FileId,
            cancellationToken: cancellationToken);

        var userMediaInfoFiles = new FileInfoModel
        {
            Id = userMediaFile.Id,
            FileName = userMediaFile.FileName,
            FileExtension = userMediaFile.FileExtension,
            OwnerUserName = userMediaFile.OwnerUserName,
            CreatedDate = userMediaFile.CreatedDate,
            UpdatedDate = userMediaFile.UpdatedDate
        };

        for (int i = 0; i < command.UserIds.Count(); i++)
        {
            var userSharedFile =
                await _sharedFilesReadRepository.Table.FirstOrDefaultAsync(x => x.UserId == command.UserIds[i],
                    cancellationToken: cancellationToken);
            if (userSharedFile != null)
            {
                var userHasFile = userSharedFile.Files.Any(f => f.Id == command.FileId);
                if (userHasFile)
                {
                    return new GenericResponse<AddMultipleSharedFilesInfoResponse>
                    {
                        Data = new AddMultipleSharedFilesInfoResponse
                        {
                            Succeeded = false
                        },
                        WarningResult = new()
                        {
                            Title = "File already shared",
                            Message = $"File already shared {userSharedFile.UserId}."
                        }
                    };
                }
            }
        }


        var listOfFiles = new List<FileInfoModel>
        {
            userMediaInfoFiles
        };


        List<SharedFilesModel> sharedFiles = new List<SharedFilesModel>();
        for (int i = 0; i < command.UserIds.Count; i++)
        {
            var sharedFile = new SharedFilesModel()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = command.UserIds[i],
                Files = listOfFiles
            };

            sharedFiles.Add(sharedFile);

            await _sharedFilesWriteRepository.AddRangeAsync(sharedFiles, cancellationToken);
            await _sharedFilesWriteRepository.SaveChangesAsync();

            return new GenericResponse<AddMultipleSharedFilesInfoResponse>
            {
                Data = new AddMultipleSharedFilesInfoResponse
                {
                    Succeeded = true
                }
            };
        }
        
        return new GenericResponse<AddMultipleSharedFilesInfoResponse>
        {
            Data = new AddMultipleSharedFilesInfoResponse
            {
                Succeeded = false
            },
            WarningResult = new()
            {
                Title = "File not shared",
                Message = "File not shared."
            }
        };
        
    }
    
}