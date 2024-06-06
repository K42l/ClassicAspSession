using System.Net;

namespace ClassicAspSession.Utility
{
    public static class CookieHelper
    {
        public static IEnumerable<Cookie> ExtractCookies(this HttpResponseMessage response)
        {
            if (!response.Headers.TryGetValues("Set-Cookie", out var cookieEntries))
                return Enumerable.Empty<Cookie>();

            var uri = response.RequestMessage.RequestUri;
            var cookieContainer = new CookieContainer();

            foreach (var cookieEntry in cookieEntries)
                cookieContainer.SetCookies(uri, cookieEntry);

            return cookieContainer.GetCookies(uri).Cast<Cookie>();
        }

        public static void PopulateCookies(this HttpRequestMessage request, IEnumerable<Cookie> cookies)
        {
            request.Headers.Remove("Cookie");
            if (cookies.Any())
                request.Headers.Add("Cookie", cookies.ToHeaderFormat());
        }

        public static string ToHeaderFormat(this IEnumerable<Cookie> cookies)
        {
            var cookieString = string.Empty;
            var delimiter = string.Empty;

            foreach (var cookie in cookies)
            {
                cookieString += delimiter + cookie;
                delimiter = "; ";
            }
            return cookieString;
        }
    }
}
