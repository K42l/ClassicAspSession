# ClassicAspSession
A Simple library to securely get the Classic ASP session variables

I was in need of a way to get the session variables from a Classic ASP Website to a ASP.NET Core Web API.
Since, for now, my only need it's to get the variables from Classic ASP to ASP.NET Core, it has only one method. But, as the necessities grow, i'll update this project to a more complete and functional state.

To use it you just need to register the interface, add a named HttpClient to use in the IHttpClientFactory and configure the HttpMessageHandler.
Just don't forget to set UseCookies to false, so the method can add the cookies directly to the header.

Ex:
``` c#
builder.Services.AddSingleton<IAspSession, AspSession>();
builder.Services.AddHttpClient("AspSession")
	.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
	{
		UseDefaultCredentials = true,
		UseCookies = false
	});
```

And the just use the GetAspSession passing the HttpContext:

``` c#
var currentContext = HttpContext.Request.HttpContext;
AspSessionModel aspSession = await _asp.GetAspSession(currentContext);
```