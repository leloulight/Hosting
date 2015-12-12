// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Server.Features;

namespace Microsoft.AspNet.Hosting
{
    public static class WebApplicationExtensions
    {
        /// <summary>
        /// Retruns the server addresses the web application is going to listen on.
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public static ICollection<string> GetAddresses(this IWebApplication application)
        {
            return application.ServerFeatures.Get<IServerAddressesFeature>()?.Addresses;
        }

        /// <summary>
        /// Runs a web application and block the calling thread until host shutdown.
        /// </summary>
        /// <param name="application"></param>
        public static void Run(this IWebApplication application)
        {
            using (application.Start())
            {
                Console.WriteLine("Hosting environment: " + application.HostingEnvironment.EnvironmentName);

                var serverAddresses = application.GetAddresses();
                if (serverAddresses != null)
                {
                    foreach (var address in serverAddresses)
                    {
                        Console.WriteLine("Now listening on: " + address);
                    }
                }

                Console.WriteLine("Application started. Press Ctrl+C to shut down.");

                Console.CancelKeyPress += (sender, eventArgs) =>
                {
                    application.Lifetime.StopApplication();

                    // Don't terminate the process immediately, wait for the Main thread to exit gracefully.
                    eventArgs.Cancel = true;
                };

                application.Lifetime.ApplicationStopping.WaitHandle.WaitOne();
            }
        }
    }
}