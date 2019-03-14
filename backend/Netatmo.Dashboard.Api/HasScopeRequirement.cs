using System;
using Microsoft.AspNetCore.Authorization;

namespace Netatmo.Dashboard.Api
{
    public class HasScopeRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; private set; }
        public string Scope { get; private set; }

        public HasScopeRequirement(string scope, string issuer)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }
    }
}
