using FluentValidation;

namespace Vision.Application.Authentication.Queries.GetUsers;

public class GetUsersValidator : AbstractValidator<GetUsersQuery>
{
    public GetUsersValidator()
    {
        RuleFor(x => x.PageNumber)
            .NotEmpty().WithMessage("Page number is required");

        RuleFor(x => x.PageSize)
            .NotEmpty().WithMessage("Page size is required");
    }
    
}