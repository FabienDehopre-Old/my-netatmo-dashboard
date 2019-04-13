using Hangfire.Annotations;
using Hangfire.Dashboard;
using JWT;
using JWT.Builder;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Collections.Generic;

namespace Netatmo.Dashboard.Tasks
{
    public class AuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly string issuer;

        public AuthorizationFilter(string issuer)
        {
            this.issuer = issuer;
        }

        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            if (httpContext.Request.Query.TryGetValue("token", out var token) && token.Count == 1)
            {
                try
                {
                    var json = new JwtBuilder()
                        .DoNotVerifySignature()
                        .Decode<IDictionary<string, object>>(token.ToString());
                    var permission = json["permissions"] as JArray;
                    var iss = json["iss"] as string;
                    return iss == issuer && permission != null && permission.Contains("");
                }
                catch (TokenExpiredException ex)
                {
                    Log.Error(ex, "Hangfire dashboard access denied because given token is expired.");
                }
                catch (SignatureVerificationException ex)
                {
                    Log.Error(ex, "Hangfire dashboard access denied because the given token is invalid (invalid signature).");
                }
            }

            return false;
        }
    }
}
