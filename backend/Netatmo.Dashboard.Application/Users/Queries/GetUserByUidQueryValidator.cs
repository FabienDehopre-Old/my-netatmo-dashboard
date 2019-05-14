using FluentValidation;

namespace Netatmo.Dashboard.Application.Users.Queries
{
    public class GetUserByUidQueryValidator : AbstractValidator<GetUserByUidQuery>
    {
        public GetUserByUidQueryValidator()
        {
            RuleFor(q => q.Uid)
                .NotEmpty();
        }
    }
}
