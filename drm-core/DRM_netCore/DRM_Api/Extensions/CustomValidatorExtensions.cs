using DRM_Api.Validators.CustomValidators;
using FluentValidation;

namespace DRM_Api.Extensions
{
	public static class CustomValidatorExtensions
	{
		public static IRuleBuilderOptions<T, string> ValidUserPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
		{
			return ruleBuilder.SetValidator(new UserPasswordValidator());
		}
	}
}