// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNet.Hosting
{
    internal class SampleApplications
    {
        // Template
        public void Main_ProjectTemplate(string[] args)
        {
            var config = WebHostConfiguration.GetDefault(args);

            var application = new WebApplicationBuilder()
                .UseConfiguration(config)
                .Build();

            application.Run();
        }

        public void Main_ConfiguringAddresses(string[] args)
        {
            var config = WebHostConfiguration.GetDefault(args);

            var application = new WebApplicationBuilder()
                .UseConfiguration(config)
                .Build();

            var addresses = application.GetAddresses();
            addresses.Add("http://localhost:5000");
            addresses.Add("http://localhost:5001");

            application.Run();
        }

        // Manual hosting and blocking
        public void Main_ControlOverWaiting(string[] args)
        {
            var config = WebHostConfiguration.GetDefault(args);

            var application = new WebApplicationBuilder()
                .UseConfiguration(config)
                .Build();

            using (var app = application.Start())
            {
                Console.ReadLine();
            }
        }

        public void Main_FullControl(string[] args)
        {
            var config = WebHostConfiguration.GetDefault(args);

            var builder = new WebApplicationBuilder()
                .UseConfiguration(config)
                .UseServerFactory("Microsoft.AspNet.Server.Kestrel")
                .UseEnvironment("Development")
                .UseWebRoot("public")
                .UseStartup(app =>
                {
                    app.Use(next => context =>
                    {
                        return next(context);
                    });
                });

            var application = builder.Build();

            application.Run();
        }


    }

    internal class StartAndStop
    {
        private readonly IWebApplication _host;
        private IDisposable _application;

        public StartAndStop()
        {
            _host = new WebApplicationBuilder().Build();
        }

        public void Start()
        {
            _application = _host.Start();
        }

        public void Stop()
        {
            _application.Dispose();
        }

        public void AddUrls()
        {
            var addresses = _host.GetAddresses();

            // Clear all addresses
            addresses.Clear();
            addresses.Add("http://localhost:5000");
        }
    }
}
