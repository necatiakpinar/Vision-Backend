using MediatR;
using Microsoft.EntityFrameworkCore;
using Vision.Application.Authentication.Common;
using Vision.Application.Repositories.SharedFiles;

namespace Vision.Application.SharedFiles.Query.Get;

public class GetSharedFileInfoHandler : IRequestHandler<GetSharedFileInfoQuery, GenericResponse<GetSharedFileInfoResponse>>
{
    private readonly ISharedFilesReadRepository _sharedFilesReadRepository;

    public GetSharedFileInfoHandler(ISharedFilesReadRepository sharedFilesReadRepository)
    {
        _sharedFilesReadRepository = sharedFilesReadRepository;
    }

    public async Task<GenericResponse<GetSharedFileInfoResponse>> Handle(GetSharedFileInfoQuery request, CancellationToken cancellationToken)
    {
        var sharedFileUser = await _sharedFilesReadRepository.Table.FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken: cancellationToken);
        
        if (sharedFileUser == null)
        {
            return new GenericResponse<GetSharedFileInfoResponse>
            {
                Data = null,
                WarningResult = new()
                {
                    Title = "No shared file user found",
                    Message = "No shared file user found for the given user id"
                }
            };
        }
        
        var response = new GetSharedFileInfoResponse
        {
            Files = sharedFileUser.Files
        };
        
        return new GenericResponse<GetSharedFileInfoResponse>
        {
            Data = response
        };
    }
}