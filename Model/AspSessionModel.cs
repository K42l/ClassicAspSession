using System.Net;

namespace ClassicAspSession.Model
{
	public class AspSessionModel
	{
		public HttpStatusCode status { get; set; }
		public string? message { get; set; }
		public Data[]? data { get; set; }

		public class Data()
		{
			public string? SessionId { get; set; }
			public Dictionary<string, string>? SessionVariables { get; set; }
		}
	}
}
