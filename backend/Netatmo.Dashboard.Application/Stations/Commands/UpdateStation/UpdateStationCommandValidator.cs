using FluentValidation;
using System.Text.RegularExpressions;

namespace Netatmo.Dashboard.Application.Stations.Commands.UpdateStation
{
    public class UpdateStationCommandValidator : AbstractValidator<UpdateStationCommand>
    {
        public UpdateStationCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Altitude).GreaterThanOrEqualTo(-413).LessThanOrEqualTo(8848);    // need to be on land (-413m in the Dead Sea Shore and 8848m on top of the Everest) ;-)
            RuleFor(x => x.City).NotEmpty().MaximumLength(256);
            RuleFor(x => x.CountryCode).NotEmpty().MinimumLength(2).MaximumLength(2);
            RuleFor(x => x.Latitude).GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90);
            RuleFor(x => x.Longitude).GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);
            RuleFor(x => x.Timezone).NotEmpty().MaximumLength(32);
            // RuleFor(x => x.StaticMap); // TOOD
        }
    }
}
