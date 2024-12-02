using FluentValidation;

namespace Vision.Application.Leaderboard.Commands.AddOrUpdateUser;

public class AddOrUpdateUserToLeaderboardCommandValidator : AbstractValidator<AddOrUpdateUserToLeaderboardCommand>
{
    public AddOrUpdateUserToLeaderboardCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required");

        RuleFor(x => x.Score)
            .NotEmpty().WithMessage("Score is required")
            .GreaterThan(0).WithMessage("Score must be greater than 0");
    }
}