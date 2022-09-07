namespace FrameContentSpecify.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseHeaderMiddleware(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            return app.UseMiddleware<HeaderMiddleware>(environment);
        }
    }
}
