using MediatR;
using Microsoft.EntityFrameworkCore;
using Vision.Application.Authentication.Common;
using Vision.Application.Repositories.OwnedFiles;

namespace Vision.Application.OwnedFiles.Query.Get;

public class GetOwnedFilesInfoHandler : IRequestHandler<GetOwnedFilesInfoCommand, GenericResponse<GetOwnedFilesInfoResponse>>
{
    private readonly IOwnedFilesReadRepository _ownedFilesReadRepository;

    public GetOwnedFilesInfoHandler(IOwnedFilesReadRepository ownedFilesReadRepository)
    {
        _ownedFilesReadRepository = ownedFilesReadRepository;
    }

    public async Task<GenericResponse<GetOwnedFilesInfoResponse>> Handle(GetOwnedFilesInfoCommand command, CancellationToken cancellationToken)
    {
        var ownerFileUser = await _ownedFilesReadRepository.Table.FirstOrDefaultAsync(x => x.OwnerId == command.OwnerId, cancellationToken: cancellationToken);
        
        if (ownerFileUser == null)
        {
            return new GenericResponse<GetOwnedFilesInfoResponse>
            {
                Data = null,
                WarningResult = new()
                {
                    Title = "No shared file user found",
                    Message = "No shared file user found for the given user id"
                }
            };
        }
        
        var response = new GetOwnedFilesInfoResponse
        {
            Files = ownerFileUser.Files
        };

        return new GenericResponse<GetOwnedFilesInfoResponse>
        {
            Data = response
        };
    }
    
}