// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Http.Features;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNet.Hosting
{
    public interface IWebApplication
    {
        IDisposable Start();

        IFeatureCollection ServerFeatures { get; }

        IServiceProvider Services { get; }

        // Services that are created by the WebHostBuilder internally
        IApplicationLifetime Lifetime { get; }

        IHostingEnvironment HostingEnvironment { get; }

        ILoggerFactory LoggerFactory { get; }
    }
}
