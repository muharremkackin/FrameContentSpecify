namespace FrameContentSpecify.Middlewares
{
    public class HeaderBuilder
    {
        private readonly HeaderPolicy _policy = new HeaderPolicy();
        private string _contentSecurityPolicy = "";

        public HeaderBuilder AddDefaultHeaderPolicy()
        {
            AddCSPFrameAncestors();

            return this;
        }

        public HeaderBuilder AddCSPFrameAncestors()
        {
            string[] domains = { "'self'", "*.birlesikodeme", "*.kitapyurdu.com" };

            _contentSecurityPolicy += "frame-ancestors " + string.Join(' ', domains);
            _policy.SetHeaders["Content-Security-Policy"] = _contentSecurityPolicy;
            return this;
        }

        public HeaderBuilder RemoveHeader(string header)
        {
            _policy.RemoveHeaders.Add(header);
            return this;
        }

        public HeaderBuilder AddHeader(string header, string value)
        {
            _policy.SetHeaders[header] = value;
            return this;
        }

        public HeaderPolicy Build()
        {
            return _policy;
        }
    }
}
