using DRM_Api.Core.Entities.DTO;
using DRM_Api.Extensions;
using FluentValidation;

namespace DRM_Api.Validators
{
	public class UserRequestValidator : AbstractValidator<UserRequest>
	{
		public UserRequestValidator()
		{
			RuleFor(x => x.Name).Length(0, 30).NotEmpty();
			RuleFor(x => x.Surname).Length(0, 30).NotEmpty();
			RuleFor(x => x.Email).Length(5, 60).EmailAddress().NotEmpty();
			RuleFor(x => x.Username).Length(3, 50).NotEmpty();
			RuleFor(x => x.Password).Length(6, 50).NotEmpty().ValidUserPassword();
			RuleFor(x => x.RoleID).NotEmpty();
		}
	}
}