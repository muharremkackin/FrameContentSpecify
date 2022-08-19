using Newtonsoft.Json;

namespace FrameContentSpecify.Middlewares
{
    public class HeaderBuilder
    {
        private readonly HeaderPolicy _policy = new HeaderPolicy();
        private string _contentSecurityPolicy = "";
        private string _contentRootPath { get; set; }

        public HeaderBuilder(string contentRootPath) { _contentRootPath = contentRootPath; }

        public HeaderBuilder AddDefaultHeaderPolicy()
        {

            AddCSPFrameAncestors();

            return this;
        }

        public HeaderBuilder AddCSPFrameAncestors()
        {
            string file = _contentRootPath + "frame-domains.json";
            using (StreamReader r = new StreamReader(file))
            {
                string jsonFrameDomains = r.ReadToEnd();
                List<string>? frameDomains = JsonConvert.DeserializeObject<List<string>>(jsonFrameDomains);

                if (frameDomains != null && frameDomains.Count() > 0)
                {
                    _contentSecurityPolicy += "frame-ancestors " + string.Join(' ', frameDomains) + ";";
                }
            }

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
