using FluentValidation;
using System.Text.RegularExpressions;

namespace Netatmo.Dashboard.Application.Countries.Queries.List
{
    public class GetTranslatedCountriesQueryValidator : AbstractValidator<GetTranslatedCountriesQuery>
    {
        public GetTranslatedCountriesQueryValidator()
        {
            RuleFor(q => q.Language)
                .NotEmpty()
                .Matches(new Regex("^Name(BR|DE|EN|ES|FA|FR|HR|IT|JA|NL|PT)$", RegexOptions.IgnoreCase));
        }
    }
}
