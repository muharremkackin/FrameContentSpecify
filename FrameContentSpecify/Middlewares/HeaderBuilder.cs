using FrameContentSpecify.Extensions;
using Newtonsoft.Json;

namespace FrameContentSpecify.Middlewares
{
    public class HeaderBuilder
    {
        private readonly HeaderPolicy _policy = new HeaderPolicy();
        private string _contentSecurityPolicy = "";
        private string _contentRootPath { get; set; }
        private HttpContext _context;

        public HeaderBuilder(HttpContext context, string contentRootPath) { 
            _context = context; 
            _contentRootPath = contentRootPath; 
        }

        public HeaderBuilder AddDefaultHeaderPolicy()
        {

            AddCSPFrameAncestors();

            return this;
        }

        public HeaderBuilder AddCSPFrameAncestors()
        {
            string file = _contentRootPath + "frame-domains.json";
            _contentSecurityPolicy += "frame-ancestors 'self' https://*.birlesikodeme.com ";
            using (StreamReader r = new StreamReader(file))
            {
                string jsonFrameDomains = r.ReadToEnd();
                List<string>? frameDomains = JsonConvert.DeserializeObject<List<string>>(jsonFrameDomains);
                
                var domain = _context.Request.Headers["Referer"].ToString().TrimEnd(new char[] { '/' });
                var collection = domain.MatchDomain();
                if (collection != null && collection.Count > 0)
                {
                    var matchedDomain = collection[0].Value.ToString();

                    Uri uri = new Uri(matchedDomain);

                    if (uri.DnsSafeHost == "localhost")
                    {
                        _contentSecurityPolicy += domain + ";";
                    } else
                    {
                        var uriScheme = uri.Scheme;
                        var domainInfo = uri.DnsSafeHost.GetDomainInfo();

                        var frameDomain = uriScheme + "://*." + domainInfo?.Domain + "." + domainInfo?.TLD;

                        if (frameDomains.Contains(frameDomain))
                        {
                            _contentSecurityPolicy += frameDomain + ";";
                        }
                        else
                        {
                            // add domain to json file
                        }
                    }

                    
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
