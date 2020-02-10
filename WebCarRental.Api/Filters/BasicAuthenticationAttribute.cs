using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebCarRental.Core.Services;

namespace WebCarRental.Api.Filters
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private readonly IUserService service;
        public BasicAuthenticationAttribute(IUserService service)
        {
            this.service = service;
        }
        private const string Realm = "My Realm";

        public override void OnAuthorization(HttpActionContext context)
        {
            if(context.Request.Headers.Authorization == null)
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.Unauthorized);

                if(context.Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    context.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", Realm));
                }

            }
            else
            {
                string authenticationToken = context.Request.Headers.Authorization.Parameter;

                string decodedAuthenticationToken = Encoding.UTF8.GetString(
                    Convert.FromBase64String(authenticationToken));

                string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');

                string username = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];

                var user = service.ValidateLogin(username, password);

                if(user != null)
                {
                    var identity = new GenericIdentity(username);

                    IPrincipal principal = new GenericPrincipal(identity, new string[] { user.Role });

                    Thread.CurrentPrincipal = principal;

                    if(HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;
                    }
                }
                else
                {   
                    context.Response = context.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}