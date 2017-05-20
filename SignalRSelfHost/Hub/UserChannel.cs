// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserChannel.cs" company="She Software Ltd">
//   Copyright (c) She Software Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SignalRSelfHost.Notifications;

namespace SignalRSelfHost.Hub
{
    public class UserChannel : Microsoft.AspNet.SignalR.Hub
    {
        public override Task OnConnected()
        {
            Debug.WriteLine("Connected");
            Clients.Client(Context.ConnectionId).sayhello("Hello " + Context.User);
            return base.OnConnected();
        }

        /// <summary>
        /// The send to the user channel - Generics aren't allowed in Signal R so we have to make use of an json string instead
        /// </summary>
        /// <param name="notification"></param>
        public void Send(string notification)
        {
            Debug.WriteLine("Authenticated... Now Sending message: " + notification);
            var notificationObj = JsonConvert.DeserializeObject<Notification>(notification);
            this.Clients.User(notificationObj.Recipient.ToString()).addMessage(notification);
        }
    }
}
