using FluentValidation;
using System.Text.RegularExpressions;

namespace Netatmo.Dashboard.Application.Countries.Queries.Single
{
    public class GetTranslatedCountryQueryValidator : AbstractValidator<GetTranslatedCountryQuery>
    {
        public GetTranslatedCountryQueryValidator()
        {
            RuleFor(q => q.Code)
                .NotEmpty()
                .Length(2);
            RuleFor(q => q.Language)
                .NotEmpty()
                .Matches(new Regex("^Name(BR|DE|EN|ES|FA|FR|HR|IT|JA|NL|PT)$", RegexOptions.IgnoreCase));
        }
    }
}
