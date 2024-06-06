using System.Net.Http.Headers;
using System.Net;
using ClassicAspSession.Utility;
using ClassicAspSession.Model;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using ClassicAspSession.Interface;

namespace ClassicAspSession
{
    public class AspSession : IAspSession
	{
		private readonly IHttpClientFactory _clientFactory;
		public AspSession(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}
		public async Task<AspSessionModel> GetAspSession(HttpContext httpContext)
		{
			var context = httpContext;
			using (HttpRequestMessage httpRequestMessage = new(HttpMethod.Get, "...aspsession.asp"))
			{
				httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
				List<Cookie> cookies = new();
				foreach (string cookieKey in context.Request.Cookies.Keys)
				{
					var cookie = context.Request.Cookies[cookieKey.ToString()];
					var addCookie = new Cookie(cookieKey, cookie);

					if (cookieKey.StartsWith("ASPSESSIONID"))
						addCookie = new Cookie(cookieKey, cookie, "/", context.Request.Host.ToString());

					cookies.Add(addCookie);
				}
				if (cookies.Count > 0)
					httpRequestMessage.PopulateCookies(cookies);

				try
				{
					using (HttpClient client = _clientFactory.CreateClient("AspSession"))
					{
						using (var response = await client.SendAsync(httpRequestMessage))
							return JsonConvert.DeserializeObject<AspSessionModel>(await response.Content.ReadAsStringAsync());
					}
				}
				catch (Exception e)
				{
					throw;
				}
			}
		}
	}
}
