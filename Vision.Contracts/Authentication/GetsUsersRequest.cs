namespace Vision.Contracts.Authentication;

public class GetsUsersRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}