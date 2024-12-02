using MediatR;
using Microsoft.EntityFrameworkCore;
using Vision.Application.Authentication.Common;
using Vision.Application.Repositories.SharedFiles;
using Vision.Domain.Entities;

namespace Vision.Application.SharedFiles.Command.Update;

public class UpdateSharedFileInfoHandler : IRequestHandler<UpdateSharedFileInfoCommand, GenericResponse<UpdateSharedFileInfoResponse>>
{
    private readonly ISharedFilesWriteRepository _sharedFilesWriteRepository;

    public UpdateSharedFileInfoHandler(ISharedFilesWriteRepository sharedFilesWriteRepository)
    {
        _sharedFilesWriteRepository = sharedFilesWriteRepository;
    }

    public async Task<GenericResponse<UpdateSharedFileInfoResponse>> Handle(UpdateSharedFileInfoCommand request, CancellationToken cancellationToken)
    {
        var sharedFileModel = await _sharedFilesWriteRepository.Table.FirstOrDefaultAsync(x => x.UserId == request.UserId,
            cancellationToken: cancellationToken);
        
        var fileInfoToRemove = sharedFileModel.Files.FirstOrDefault(x => x.Id == request.FileId);

        if (fileInfoToRemove == null)
        {
            return new GenericResponse<UpdateSharedFileInfoResponse>()
            {
                WarningResult = new()
                {
                    Title = "File is not exist!",
                    Message = "File is not exist with that ID"
                }
            };
        }

        sharedFileModel.Files.Remove(fileInfoToRemove);

        await _sharedFilesWriteRepository.SaveChangesAsync();

        return new GenericResponse<UpdateSharedFileInfoResponse>()
        {
            Data = new()
            {
                Succeeded = true
            }
        };

        
    }
}