using EuroMobileTest.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace EuroMobileTest.Test
{
    internal class CoordinatesApiApplication : WebApplicationFactory<CoordinatesController>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            return base.CreateHost(builder);
        }
    }
}
