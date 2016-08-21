using Microsoft.Owin;
using WebAPI2.SwashBuckle.Multitenancy;

[assembly: OwinStartup(typeof(Startup))]
namespace WebAPI2.SwashBuckle.Multitenancy
{
    using IdentityServer3.AccessTokenValidation;
    using Owin;
    using Swashbuckle.Application;
    using System.IdentityModel.Tokens;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Cors;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            var cors = new EnableCorsAttribute("*", "*", "*");
            httpConfiguration.EnableCors(new EnableCorsAttribute("https://localhost:44333", "accept, authorization", "GET", "WWW-Authenticate"));

            // Requires Authorization
            JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "https://localhost:44333/core",
                RequiredScopes = new[] { "openid", "profile", "email", "tenant" },
                PreserveAccessToken = true
            });

            httpConfiguration.Filters.Add(new AuthorizeAttribute());
            httpConfiguration.Formatters.Remove(httpConfiguration.Formatters.XmlFormatter);

            // Swagger
            httpConfiguration.EnableSwagger(
                c =>
                {
                    c.SingleApiVersion("v1", "WebAPI 2 SwashBuckle Multitenancy");
                })
            .EnableSwaggerUi(
                c =>
                {
                    /* Extending the Swagger functionality to work with Identity Server */
                    c.CustomAsset("index", Assembly.GetAssembly(typeof(Startup)), "WebAPI2.SwashBuckle.Multitenancy.SwaggerExtensions.index.html");
                    c.CustomAsset("SwaggerExtensions/enabletokenclient.js", Assembly.GetAssembly(typeof(Startup)), "WebAPI2.SwashBuckle.Multitenancy.SwaggerExtensions.enabletokenclient.js");
                    c.CustomAsset("SwaggerExtensions/oidc-client.min.js", Assembly.GetAssembly(typeof(Startup)), "WebAPI2.SwashBuckle.Multitenancy.SwaggerExtensions.oidc-client.min.js");
                });


            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            httpConfiguration.MapHttpAttributeRoutes();

            /* Routing startup page to swagger */
            httpConfiguration.Routes.MapHttpRoute(
                name: "swagger_root",
                routeTemplate: string.Empty,
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(message => message.RequestUri.ToString(), "swagger"));

            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(httpConfiguration);
        }
    }
}
