// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserIdProvider.cs" company="She Software Ltd">
//   Copyright (c) She Software Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Security.Claims;
using Microsoft.AspNet.SignalR;

namespace SignalRSelfHost.Providers
{
    public class UserIdProvider : IUserIdProvider
    {
        /// <summary>
        /// Gets the User id from the authenticated user and assigns it to the connection as an identifier
        /// </summary>
        /// <param name="request">the request </param>
        /// <returns>the user id</returns>
        public string GetUserId(IRequest request)
        {
            var claimsPrincipal = request.User as ClaimsPrincipal;
            if (claimsPrincipal != null)
            {
                switch (claimsPrincipal.FindFirst("client_id").Value)
                {
                    case "clientcredentials.client":
                        return null;
                        break;
                    case "customgrant.client":
                        return claimsPrincipal.FindFirst("sub").Value;
                    default:
                        throw new ArgumentException("Unknown client id.  If coming from another device please specify it in the Identity Server configuration and give it access to the SignalR scope");
                }
            }

            return null;
        }
    }
}