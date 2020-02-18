using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DRM_Api.Extensions
{
	public static class ModelStateExtension
	{
		public static List<string> GetErrorMessages(this  ModelStateDictionary dictionary)
		{
			return dictionary.SelectMany(x => x.Value.Errors)
				.Select(x => x.ErrorMessage)
				.ToList();
		}
	}
}