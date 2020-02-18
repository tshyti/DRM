using DRM_Api.Core.Entities.DTO;
using FluentValidation;

namespace DRM_Api.Validators
{
	public class UserFileUploadDataValidator : AbstractValidator<UserFileUploadData>
	{
		public UserFileUploadDataValidator()
		{
			RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
			RuleFor(x => x.MimeType).NotEmpty().MaximumLength(100);
			RuleFor(x => x.Data).NotEmpty();
		}
	}
}