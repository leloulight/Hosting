using Microsoft.AspNet.Hosting;

namespace Microsoft.AspNet.TestHost
{
    public static class WebApplicationBuilderExtensions
    {
        public static TestServer BuildTestServer(this WebApplicationBuilder builder)
        {
            return new TestServer(builder);
        }
    }

}
