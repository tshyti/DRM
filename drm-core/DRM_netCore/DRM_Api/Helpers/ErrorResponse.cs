using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DRM_Api.Helpers
{
	public class ErrorResponse<TEntity> where TEntity : class
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public TEntity ErrorObject { get; set; }
	}
}
