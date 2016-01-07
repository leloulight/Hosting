// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.FileProviders;
using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNet.Hosting.Internal
{
    public class HostingEnvironment : IHostingEnvironment
    {
        public string EnvironmentName { get; set; } = Microsoft.AspNet.Hosting.EnvironmentName.Production;

        public string WebRootPath { get; set; }

        public IFileProvider WebRootFileProvider { get; set; }

        public IConfiguration Configuration { get; set; }
    }
}