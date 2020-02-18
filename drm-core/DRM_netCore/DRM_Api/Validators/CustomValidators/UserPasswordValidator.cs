using System.Linq;
using FluentValidation.Validators;

namespace DRM_Api.Validators.CustomValidators
{
	public class UserPasswordValidator : PropertyValidator
	{
		public UserPasswordValidator() : base("'Password' must contain at least one number, uppercase and symbol.")
		{

		}

		protected override bool IsValid(PropertyValidatorContext context)
		{
			string passwordValue = (string)context.PropertyValue;
			return (passwordValue.Any(char.IsUpper) && passwordValue.Any(char.IsDigit) &&
			        (passwordValue.Any(char.IsSymbol) || passwordValue.Any(char.IsPunctuation)));
		}
	}
}