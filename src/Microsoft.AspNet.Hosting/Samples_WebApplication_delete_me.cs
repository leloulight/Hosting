// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNet.Hosting
{
    internal class SampleApplications
    {
        // Template
        public void Main_ProjectTemplate(string[] args)
        {
            var config = WebApplicationConfiguration.GetDefault(args);

            var application = new WebApplicationBuilder()
                .UseConfiguration(config)
                .Build();

            application.Run();
        }

        public void Main_ConfiguringAddresses(string[] args)
        {
            var config = WebApplicationConfiguration.GetDefault(args);

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
            var config = WebApplicationConfiguration.GetDefault(args);

            var application = new WebApplicationBuilder()
                .UseConfiguration(config)
                .Build();

            using (var app = application.Start())
            {
                Console.ReadLine();
            }
        }

        public void Main_FullControl()
        {
            var builder = new WebApplicationBuilder()
                .UseServerFactory("Microsoft.AspNet.Server.Kestrel") // Set the server manually
                .UseEnvironment("Development")
                .UseWebRoot("public")
                .UseServices(services =>
                {
                    // Configure services that the application can see
                    services.AddSingleton<IMyCustomService, MyCustomService>();
                })
                .UseStartup(app =>
                {
                    // Write the application inline, this won't call any startup class in the assembly

                    app.Use(next => context =>
                    {
                        return next(context);
                    });
                });

            var application = builder.Build();

            // Add custom logger provider
            application.LoggerFactory.AddProvider(new MyHostLoggerProvider());

            application.Run();
        }

        public class MyHostLoggerProvider : ILoggerProvider
        {
            public ILogger CreateLogger(string categoryName)
            {
                throw new NotImplementedException();
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }

        public interface IMyCustomService
        {
            void Go();
        }

        public class MyCustomService : IMyCustomService
        {
            public void Go()
            {
                throw new NotImplementedException();
            }
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
