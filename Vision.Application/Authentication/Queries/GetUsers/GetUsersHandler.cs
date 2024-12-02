using MediatR;
using Microsoft.EntityFrameworkCore;
using Vision.Application.Authentication.Common;
using Vision.Application.Common;
using Vision.Application.Repositories.User;
using Vision.Domain.Identity;

namespace Vision.Application.Authentication.Queries.GetUsers;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, GenericResponse<GetUsersResponse>>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly int _minPageSize = 1;

    public GetUsersHandler(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task<GenericResponse<GetUsersResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        if (request.PageNumber < _minPageSize || request.PageSize < _minPageSize)
        {
            return new GenericResponse<GetUsersResponse>()
            {
                WarningResult = new WarningResult()
                {
                    Title = "Invalid Page Number or Page Size",
                    Message = "Page number or size must be greater than 0"
                }
            };
        }

        IQueryable<User> query = _userReadRepository.Table;

        var users = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);
        
        return new GenericResponse<GetUsersResponse>()
        {
            Data = new GetUsersResponse()
            {
                Users = users,
                TotalCount = totalCount,
            }
        };
    }
}