// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="She Software Ltd">
//   Copyright (c) She Software Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Microsoft.Owin.Hosting;

namespace SignalRSelfHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string url = "http://*:8080";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Server running on {0}", url);
                Console.ReadLine();
            }
        }
    }
}
