using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using GeneralCommands.Models;

namespace GeneralCommands.Extensions
{
    public static class RegexExtension
    {
        private static List<RegexModel> _regexInformations = new List<RegexModel>
        {
            new RegexModel {Name = "Domain", Value = @"^(?:https?:\/\/)?(?:[^@\/\n]+@)?(?:www\.)?([^:\/?\n]+)" }
        };


        public static MatchCollection? MatchDomain(this string domain)
        {
            var regex = _regexInformations.Find(m => m.Name == "Domain").Value;
            if (regex != null)
            {
                return Regex.Matches(domain, _regexInformations.Find(m => m.Name == "Domain").Value);
            }
            return null;
        }
    }
}
