using FluentValidation;

namespace Vision.Application.Leaderboard.Queries.GetLeaderboardUsers;

public class GetLeaderboardUsersValidator : AbstractValidator<GetLeaderboardUsersQuery>
{
    public GetLeaderboardUsersValidator()
    {
        RuleFor(x => x.PageNumber)
            .NotEmpty().WithMessage("Page number is required");

        RuleFor(x => x.PageSize)
            .NotEmpty().WithMessage("Page size is required");
    }
    
}