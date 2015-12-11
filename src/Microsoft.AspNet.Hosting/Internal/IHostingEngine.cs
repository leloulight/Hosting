// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Server.Features;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNet.Hosting
{
    public static class HostingEngineExtensions
    {
        public static ICollection<string> GetAddresses(this IWebApplication application)
        {
            return application.ServerFeatures.Get<IServerAddressesFeature>().Addresses;
        }

        public static void Run(this IWebApplication application)
        {
            using (application.Start())
            {
                var hostingEnv = application.Services.GetRequiredService<IHostingEnvironment>();
                Console.WriteLine("Hosting environment: " + hostingEnv.EnvironmentName);

                var serverAddresses = application.ServerFeatures.Get<IServerAddressesFeature>();
                if (serverAddresses != null)
                {
                    foreach (var address in serverAddresses.Addresses)
                    {
                        Console.WriteLine("Now listening on: " + address);
                    }
                }

                Console.WriteLine("Application started. Press Ctrl+C to shut down.");

                var appLifetime = application.Services.GetRequiredService<IApplicationLifetime>();

                Console.CancelKeyPress += (sender, eventArgs) =>
                {
                    appLifetime.StopApplication();
                    // Don't terminate the process immediately, wait for the Main thread to exit gracefully.
                    eventArgs.Cancel = true;
                };

                appLifetime.ApplicationStopping.WaitHandle.WaitOne();
            }
        }
    }
}