// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorizationQsTokenExtractorMiddleware.cs" company="She Software Ltd">//   
//   Copyright (c) She Software Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Owin;

namespace SignalRSelfHost.Auth
{
    /// <summary>
    /// We use this middleware to add Authorization headers for use later down the pipeline.  It allows the IdentityServerBearerTokenAuthentication
    /// library to work the way it was built to and gives us access to the User via the Principal. 
    /// </summary>
    public class AuthorizationQsTokenExtractorMiddleware : OwinMiddleware
    {
        public AuthorizationQsTokenExtractorMiddleware(OwinMiddleware next) : base(next)
        {
        }

        /// <summary>
        /// Extracting the Authorization info from the query string and adding it to the headers.
        /// </summary>
        /// <param name="context">The context</param>
        /// <returns>The task</returns>
        public override async Task Invoke(IOwinContext context)
        {
            var bearerToken = context.Request.Query.Get("access_token");

            if (!string.IsNullOrEmpty(bearerToken))
            {
                string[] authorization = { "Bearer " + bearerToken };
                context.Request.Headers.Add("Authorization", authorization);
            }

            await Next.Invoke(context);
        }
    }
}