
namespace Yenilen.API.Middlewares;

public static class ExceptionHandlingExtensions
{
   public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
   {
      return app.UseMiddleware<ExceptionHandlingMiddleware>();
   }
}