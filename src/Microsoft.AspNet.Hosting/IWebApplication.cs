// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNet.Http.Features;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNet.Hosting
{
    /// <summary>
    /// Represents a configured web application
    /// </summary>
    public interface IWebApplication
    {
        /// <summary>
        /// The <see cref="IFeatureCollection"/> exposed by the configured server.
        /// </summary>
        IFeatureCollection ServerFeatures { get; }

        /// <summary>
        /// The <see cref="IServiceProvider"/> for the application.
        /// </summary>
        IServiceProvider Services { get; }

        /// <summary>
        /// 
        /// </summary>
        IApplicationLifetime Lifetime { get; }

        /// <summary>
        /// The <see cref="IHostingEnvironment"/> of the application.
        /// </summary>
        IHostingEnvironment HostingEnvironment { get; }

        /// <summary>
        /// The <see cref="ILoggerFactory"/> 
        /// </summary>
        ILoggerFactory LoggerFactory { get; }

        /// <summary>
        /// Starts listening on the configured addresses.
        /// </summary>
        /// <returns></returns>
        IDisposable Start();
    }
}
