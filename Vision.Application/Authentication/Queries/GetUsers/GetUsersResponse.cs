using Vision.Application.Authentication.Common.Interfaces;
using Vision.Application.Common;
using Vision.Domain.Identity;

namespace Vision.Application.Authentication.Queries.GetUsers;

public class GetUsersResponse : IResponse
{
    public List<User> Users { get; set; }
    public  int TotalCount { get; set; }
    public WarningResult? WarningResult { get; set; }
}