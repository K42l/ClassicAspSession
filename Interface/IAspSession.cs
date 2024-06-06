using ClassicAspSession.Model;
using Microsoft.AspNetCore.Http;

namespace ClassicAspSession.Interface
{
	public interface IAspSession
	{
		Task<AspSessionModel> GetAspSession(HttpContext httpContext);
	}
}
