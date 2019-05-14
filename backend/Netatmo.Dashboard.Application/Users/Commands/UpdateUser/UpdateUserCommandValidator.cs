using FluentValidation;
using System.Text.RegularExpressions;

namespace Netatmo.Dashboard.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.AccessToken).NotEmpty().When(x => x.ExpiresAt.HasValue);
            RuleFor(x => x.RefreshToken).NotEmpty().When(x => x.ExpiresAt.HasValue);
            RuleFor(x => x.ExpiresAt).NotNull().When(x => !string.IsNullOrEmpty(x.AccessToken));
            RuleFor(x => x.UpdateJobId).Matches(@"^fetch\-update\-[0-9a-f]{8}\-([0-9a-f]{4}\-){3}[0-9a-f]{12}$").When(x => x.UpdateJobId != null);
            RuleFor(x => x.FeelLike.Value).IsInEnum().When(x => x.FeelLike.HasValue);
            RuleFor(x => x.PressureUnit.Value).IsInEnum().When(x => x.PressureUnit.HasValue);
            RuleFor(x => x.Unit.Value).IsInEnum().When(x => x.Unit.HasValue);
            RuleFor(x => x.WindUnit.Value).IsInEnum().When(x => x.WindUnit.HasValue);
        }
    }
}
