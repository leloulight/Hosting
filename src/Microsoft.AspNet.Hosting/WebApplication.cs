// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNet.Hosting
{

    public class WebHostConfiguration
    {
        public static readonly string HostingJsonFile = "hosting.json";
        public static readonly string EnvironmentVariablesPrefix = "ASPNET_";

        public static IConfiguration GetDefault()
        {
            return GetDefault(args: null);
        }

        public static IConfiguration GetDefault(string[] args)
        {
            // We are adding all environment variables first and then adding the ASPNET_ ones
            // with the prefix removed to unify with the command line and config file formats
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile(HostingJsonFile, optional: true)
                .AddEnvironmentVariables()
                .AddEnvironmentVariables(prefix: EnvironmentVariablesPrefix);

            if (args != null)
            {
                configBuilder.AddCommandLine(args);
            }

            return configBuilder.Build();
        }
    }

    internal class Sample
    {
        public void Main(string[] args)
        {
            // var config = WebHostConfiguration.GetDefault(args);
            var server = args[0];

            var config = new ConfigurationBuilder()
                            .AddJsonFile($"hosting.{server}.json", optional: true)
                            .Build();

            var builder = new WebApplicationBuilder()
                .UseConfiguration(config)
                .UseServerFactory("Microsoft.AspNet.Server.Kestrel")
                .UseEnvironment("Development")
                .UseStartup(appBuilder =>
                {
                });

            // var engine = builder.Start();
            var application = builder.Build();
        }

        // Template
        public void Main2(string[] args)
        {
            var config = WebHostConfiguration.GetDefault(args);

            var application = new WebApplicationBuilder()
                .UseConfiguration(config)
                .Build();

            application.Run();
        }

        public void Main3(string[] args)
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
        public void Main4(string[] args)
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
    }

    internal class StartAndStop
    {
        private readonly IWebApplication _host;
        private IDisposable _app;

        public StartAndStop()
        {
            _host = new WebApplicationBuilder().Build();
        }

        public void Start()
        {
            _app = _host.Start();
        }

        public void Stop()
        {
            _app.Dispose();
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
