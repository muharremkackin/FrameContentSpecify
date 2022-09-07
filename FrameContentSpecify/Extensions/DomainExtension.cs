using Nager.PublicSuffix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameContentSpecify.Extensions
{
    public static class DomainExtension
    {
        public static DomainInfo? GetDomainInfo(this string url)
        {
            var domainParser = new DomainParser(new WebTldRuleProvider());
            var domainName = domainParser.Parse(url);

            return domainName;
        }
    }
}
