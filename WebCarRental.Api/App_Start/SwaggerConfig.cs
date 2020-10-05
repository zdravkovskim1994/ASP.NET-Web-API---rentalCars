using System.Web.Http;
using WebActivatorEx;
using WebCarRental.Api;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebCarRental.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {   
                        c.SingleApiVersion("v1", "WebCarRental.Api");
                    })
                .EnableSwaggerUi(c =>
                    {
                        
                    });
        }
    }
}
