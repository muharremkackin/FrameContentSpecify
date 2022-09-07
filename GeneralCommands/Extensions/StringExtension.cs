using Nager.PublicSuffix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommands.Extensions
{
    public static class StringExtension
    {
        public static DomainInfo? GetSubdomain(this string url)
        {
            var domainParser = new DomainParser(new WebTldRuleProvider());
            var domainName = domainParser.Parse(url);

            return domainName;
        }
    }
}
