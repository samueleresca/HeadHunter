using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace HeadHunter.Web.Infrastructure.Middleware
{
    public static class RequestCultureMiddleware
    {
        public static IApplicationBuilder UseRequestCultureMiddleware(this IApplicationBuilder app,
            Action<IRouteBuilder> configureRoutes)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var routes = new RouteBuilder(app)
            {
                DefaultHandler = app.ApplicationServices.GetRequiredService<MvcRouteHandler>(),
            };
            configureRoutes(routes);
            routes.Routes.Insert(0, AttributeRouting.CreateAttributeMegaRoute(app.ApplicationServices));
            var router = routes.Build();

            return app.UseMiddleware<GetRoutesMiddleware>(router);
        }
    }

    public class GetRoutesMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IRouter _router;

        public GetRoutesMiddleware(RequestDelegate next, IRouter router)
        {
            this.next = next;
            _router = router;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var context = new RouteContext(httpContext);
            context.RouteData.Routers.Add(_router);

            await _router.RouteAsync(context);

            if (context.Handler != null)
            {
                httpContext.Features[typeof(IRoutingFeature)] = new RoutingFeature()
                {
                    RouteData = context.RouteData,
                };
                var culture = context.RouteData.Values["language"].ToString();

                if (culture == null || !culture.Contains("-") || culture.Length != 5) await next(httpContext);

                var cultureInfo = new CultureInfo(context.RouteData.Values["language"].ToString());
                CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            }

            await next(httpContext);
        }
    }
}

