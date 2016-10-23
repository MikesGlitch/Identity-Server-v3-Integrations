// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Notification.cs" company="She Software Ltd">//   
//   Copyright (c) She Software Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SignalRClient.Notifications
{
    public class Notification
    {
        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the Recipient
        /// </summary>
        public int Recipient { get; set; }
    }
}