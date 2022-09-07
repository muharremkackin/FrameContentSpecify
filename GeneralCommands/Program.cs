using GeneralCommands.Extensions;

var collection = "https://app.kitapyurdu.co.uk".MatchDomain();

if (collection != null && collection.Count > 0)
{
    var domain = collection[0].Value.ToString();

    Uri uri = new Uri(domain);

    var uriScheme = uri.Scheme;
    var domainInfo = uri.DnsSafeHost.GetSubdomain();

    Console.WriteLine( uriScheme + "://*." + domainInfo?.Domain + "." + domainInfo?.TLD);
}