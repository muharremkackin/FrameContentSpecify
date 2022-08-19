namespace FrameContentSpecify.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseHeaderMiddleware(this IApplicationBuilder app, HeaderBuilder builder)
        {
            HeaderPolicy policy = builder.Build();
            return app.UseMiddleware<HeaderMiddleware>(policy);
        }
    }
}
