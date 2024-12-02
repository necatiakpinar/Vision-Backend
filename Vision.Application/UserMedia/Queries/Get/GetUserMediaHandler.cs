using MediatR;
using Microsoft.EntityFrameworkCore;
using Vision.Application.Authentication.Common;
using Vision.Application.Common;
using Vision.Application.Repositories.UserMedia;
using Vision.Domain.Entities;

namespace Vision.Application.UserMedia.Queries.Get;

public class GetUserMediaHandler : IRequestHandler<GetUserMediaQuery, GenericResponse<GetUserMediaResponse>>
{
    private readonly IUserMediaReadRepository _userMediaReadRepository;
    
    public GetUserMediaHandler(IUserMediaReadRepository userMediaReadRepository)
    {
        _userMediaReadRepository = userMediaReadRepository;
    }

    public async Task<GenericResponse<GetUserMediaResponse>> Handle(GetUserMediaQuery ınfoQuery, CancellationToken cancellationToken)
    {
        var userMediaFiles = await _userMediaReadRepository.Table.Where(x => x.Id == ınfoQuery.FileId).ToListAsync(cancellationToken);

        if (userMediaFiles.Count == 0)
        {
            return new GenericResponse<GetUserMediaResponse>
            {
                Data = null,
                WarningResult = new()
                {
                    Title = "No media found",
                    Message = "No media found for this user",
                }
            };
        }

        var response = new GetUserMediaResponse
        {
            MediaFiles = userMediaFiles
        };

        return new GenericResponse<GetUserMediaResponse>
        {
            Data = response
        };
    }
}