using MediatR;
using Microsoft.EntityFrameworkCore;
using Vision.Application.Authentication.Common;
using Vision.Application.Repositories.SharedFiles;
using Vision.Application.Repositories.UserMedia;
using Vision.Domain.Entities;

namespace Vision.Application.SharedFiles.Command.Add;

public class AddSharedFilesInfoHandler : IRequestHandler<AddSharedFilesInfoCommand, GenericResponse<AddSharedFilesInfoResponse>>
{
    public readonly ISharedFilesWriteRepository _sharedFilesWriteRepository;
    public readonly ISharedFilesReadRepository _sharedFilesReadRepository;
    public readonly IUserMediaReadRepository _userMediaReadRepository;

    public AddSharedFilesInfoHandler(ISharedFilesWriteRepository sharedFilesWriteRepository, ISharedFilesReadRepository sharedFilesReadRepository, IUserMediaReadRepository userMediaReadRepository)
    {
        _sharedFilesWriteRepository = sharedFilesWriteRepository;
        _sharedFilesReadRepository = sharedFilesReadRepository;
        _userMediaReadRepository = userMediaReadRepository;
    }


    public async Task<GenericResponse<AddSharedFilesInfoResponse>> Handle(AddSharedFilesInfoCommand command, CancellationToken cancellationToken)
    {
        var userMediaFile = await _userMediaReadRepository.Table.FirstOrDefaultAsync(x => x.Id == command.FileId, cancellationToken: cancellationToken);
        
        var userMediaInfoFiles = new FileInfoModel
        {
            Id = userMediaFile.Id,
            FileName = userMediaFile.FileName,
            FileExtension = userMediaFile.FileExtension,
            OwnerUserName = userMediaFile.OwnerUserName,
            CreatedDate = userMediaFile.CreatedDate,
            UpdatedDate = userMediaFile.UpdatedDate
        };
        
        var sharedFiles = await _sharedFilesReadRepository.Table.Where(x => x.UserId == command.UserId).ToListAsync(cancellationToken);
        if (sharedFiles.Count == 0)
        {
            var listOfFiles = new List<FileInfoModel>
            {
                userMediaInfoFiles
            };

            var sharedFile = new SharedFilesModel()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = command.UserId,
                Files = listOfFiles
            };
            
            await _sharedFilesWriteRepository.AddAsync(sharedFile, cancellationToken);
            await _sharedFilesWriteRepository.SaveChangesAsync();
            
            return new GenericResponse<AddSharedFilesInfoResponse>
            {
                Data = new AddSharedFilesInfoResponse
                {
                    Succeeded = true
                }
            };
        }

        var getUserSharedFiles = await _sharedFilesReadRepository.Table.FirstOrDefaultAsync(x => x.UserId == command.UserId, cancellationToken: cancellationToken);
        if (getUserSharedFiles == null)
        {
            return new GenericResponse<AddSharedFilesInfoResponse>
            {
                Data = new AddSharedFilesInfoResponse
                {
                    Succeeded = false
                },
                WarningResult = new()
                {
                    Title = "No shared files found",
                    Message = "No shared files found for this user",
                }
            };
        }
        
        getUserSharedFiles.Files.Add(userMediaInfoFiles);
        
        _sharedFilesWriteRepository.Update(getUserSharedFiles);
        await _sharedFilesWriteRepository.SaveChangesAsync();
        
        return new GenericResponse<AddSharedFilesInfoResponse>
        {
            Data = new AddSharedFilesInfoResponse
            {
                Succeeded = true
            }
        };
    }
}