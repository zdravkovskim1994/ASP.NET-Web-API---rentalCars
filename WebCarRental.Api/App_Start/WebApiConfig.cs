using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using WebCarRental.Core.Repository;
using WebCarRental.Core.Services;
using log4net;
using WebCarRental.Api.Filters;

namespace WebCarRental.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            #region Dependency Injection
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<DatabaseRepo>().As<IDatabaseRepo>();
            builder.RegisterType<CarService>().As<ICarService>();
            builder.RegisterType<CustomerService>().As<ICustomerService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.Register(c => LogManager.GetLogger(typeof(object))).As<ILog>();

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            #endregion

            config.Filters.Add(new BasicAuthenticationAttribute(container.Resolve<IUserService>()));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
