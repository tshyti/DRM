using DRM_Api.Core.Entities.DTO;
using FluentValidation;

namespace DRM_Api.Validators
{
	public class TokenRefreshRequestValidator : AbstractValidator<TokenRefreshRequest>
	{
		public TokenRefreshRequestValidator()
		{
			RuleFor(x => x.AccessToken).NotEmpty();
			RuleFor(x => x.RefreshToken).NotEmpty();
		}
	}
}