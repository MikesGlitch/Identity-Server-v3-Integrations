using System;
using System.Configuration;
using IdentityModel.Client;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using SignalRSelfHost.Notifications;

namespace SignalRClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new HubConnection("http://localhost:8080/signalr/", $"access_token={ GetClientToken().AccessToken }");

            var userChannel = connection.CreateHubProxy("userChannel");
            connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("Failed to start: {0}", task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Success! Connected with client connection id {0}", connection.ConnectionId);
                    var notification = new Notification { Message = "test notification", Recipient = 2, Type = "TheType" };
                    var notificationStr = JsonConvert.SerializeObject(notification);
                    userChannel.Invoke<string>("Send", notificationStr).ContinueWith(sendTask =>
                    {
                        if (task.IsFaulted)
                        {
                            Console.WriteLine("There was an error calling send: {0}", sendTask.Exception.GetBaseException());
                        }
                        else
                        {
                            Console.WriteLine(sendTask.Result);
                        }
                    }).Wait();
                }
            });

            Console.ReadLine();
        }


        private static TokenResponse GetClientToken()
        {
            var client = new TokenClient(ConfigurationManager.AppSettings["IdentityServerTokenURL"], "clientcredentials.client", "61B754C541BBCFC6A45A9E9EC5E47D8702B78C29");
            return client.RequestClientCredentialsAsync("signalR").Result;
        }
    }
}
