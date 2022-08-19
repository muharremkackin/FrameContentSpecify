namespace FrameContentSpecify.Middlewares
{
    public class HeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HeaderPolicy _policy;

        public HeaderMiddleware(RequestDelegate next, HeaderPolicy policy)
        {
            _next = next;
            _policy = policy;
        }
        
        public async Task Invoke(HttpContext context)
        {
            IHeaderDictionary headers = context.Response.Headers;
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
