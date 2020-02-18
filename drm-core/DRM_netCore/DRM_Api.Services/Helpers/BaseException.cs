using System;

namespace DRM_Api.Services.Helpers
{
	public class BaseException : Exception
	{
		public int StatusCode { get; set; }
		public string Details { get; set; }public BaseException(int statusCode, string messsage) : base(messsage)
		{
			StatusCode = statusCode;
		}

		public BaseException() : base()
		{

		}

		public BaseException(int statusCode) : base()
		{
			StatusCode = statusCode;
		}

		public BaseException(int statusCode, string messsage, string details) : base(messsage)
		{
			StatusCode = statusCode;
			Details = details;
		}

		private string GetMessageFromStatusCode(int statusCode)
		{
			switch (statusCode)
			{
				case 400: 
					return "Bad request";
				case 404:
					return "Resource not found";
				case 500:
					return "An unhandled error occurred";
				default:
					return null;
			}
		}
	}
}
