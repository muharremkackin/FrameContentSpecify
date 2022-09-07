namespace FrameContentSpecify.Middlewares
{
    public class HeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private HeaderPolicy _policy;
        private readonly IWebHostEnvironment _environment;

        public HeaderMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _environment = env;
        }
        
        public async Task Invoke(HttpContext context)
        {
            IHeaderDictionary headers = context.Response.Headers;
            _policy = (new HeaderBuilder(context: context, contentRootPath: _environment.ContentRootPath)).AddDefaultHeaderPolicy().Build();

            foreach (var headerValuePair in _policy.SetHeaders)
            {
                headers[headerValuePair.Key] = headerValuePair.Value;
            }
            foreach (var removeHeaderValuePair in _policy.RemoveHeaders)
            {
                headers.Remove(removeHeaderValuePair);
            }
            await _next(context);
        }
    }
}
