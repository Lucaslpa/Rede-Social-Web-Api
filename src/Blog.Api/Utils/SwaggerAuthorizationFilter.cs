using Blog.Business.Models;
using System.Security.Claims;

namespace Blog.api.Utils
{
    public class SwaggerAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public SwaggerAuthenticationMiddleware( RequestDelegate next )
        {
            _next = next;
        }

        public async Task Invoke( HttpContext context )

        {
            if(!context.Request.Path.StartsWithSegments( "/swagger" ))
            {
                await _next( context );
                return;
            }

          
                var Dev = context.User.Claims.Any( c => c.Type == ClaimTypes.Role && c.Value == "Dev" );
                var authorized = context.User.Identity.IsAuthenticated && Dev;
                if (authorized)
                {
                    await _next( context );
                    return;
                }
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
           
        }
    }

    public static class SwaggerAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseSwaggerAuthentication( this IApplicationBuilder builder )
        {
            return builder.UseMiddleware<SwaggerAuthenticationMiddleware>();
        }
    }
}
