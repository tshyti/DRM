using DRM_Api.Core.Entities.DTO;
using FluentValidation;

namespace DRM_Api.Validators
{
	public class AuhenticateModelValidator : AbstractValidator<AuthenticateModel>
	{
		public AuhenticateModelValidator()
		{
			RuleFor(x => x.Username).NotEmpty();
			RuleFor(x => x.Password).NotEmpty();
		}
	}
}