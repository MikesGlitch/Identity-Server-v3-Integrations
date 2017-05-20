// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="She Software Ltd">
//   Copyright (c) She Software Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Configuration;
using System.IdentityModel.Tokens;
using IdentityServer3.AccessTokenValidation;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;
using SignalRSelfHost.Auth;
using SignalRSelfHost.Providers;

namespace SignalRSelfHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.Use<AuthorizationQsTokenExtractorMiddleware>();

            JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();
            app.UseIdentityServerBearerTokenAuthentication(
               new IdentityServerBearerTokenAuthenticationOptions
               {
                   Authority = ConfigurationManager.AppSettings["IdentityServerURL"],
                   RequiredScopes = new[] { "signalR" },
                   PreserveAccessToken = true,
               });

            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new UserIdProvider());
            var hubConfiguration = new HubConfiguration { EnableDetailedErrors = true };
            app.MapSignalR(hubConfiguration);

            GlobalHost.HubPipeline.RequireAuthentication();  //// Require auth for everything in the pipeline

            /* Clean up unused references, also make sure signalr hub and assure is seperated as much as posisble */
        }
    }
}
