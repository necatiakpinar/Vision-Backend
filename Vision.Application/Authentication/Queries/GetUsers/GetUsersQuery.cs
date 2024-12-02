using MediatR;
using Vision.Application.Authentication.Common;

namespace Vision.Application.Authentication.Queries.GetUsers;

public class GetUsersQuery : IRequest<GenericResponse<GetUsersResponse>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}