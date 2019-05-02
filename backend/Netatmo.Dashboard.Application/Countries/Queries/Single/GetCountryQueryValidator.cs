using FluentValidation;

namespace Netatmo.Dashboard.Application.Countries.Queries.Single
{
    public class GetCountryQueryValidator : AbstractValidator<GetCountryQuery>
    {
        public GetCountryQueryValidator()
        {
            RuleFor(q => q.Code)
                .NotEmpty()
                .Length(2);
        }
    }
}
