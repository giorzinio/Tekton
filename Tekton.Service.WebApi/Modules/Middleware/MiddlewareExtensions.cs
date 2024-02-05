using Tekton.Service.WebApi.Modules.GlobalException;

namespace Tekton.Service.WebApi.Modules.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionHandler>();
        }
    }
}
