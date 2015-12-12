using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

}
