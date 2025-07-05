namespace FileManagement.Api.Contract;

public class UploadedImageequestValidator : AbstractValidator<UploadedImageRequest>
{
	public UploadedImageequestValidator()
	{
		RuleFor(x => x.Image.FileName)
			.SetValidator(new AllowdFileNameValidator());

		RuleFor(x => x.Image)
			.SetValidator(new FileSizeValidator())
			.SetValidator(new BlockedSignaturesValidator())
			.Must((request, context) =>
			{
				var extension = Path.GetExtension(request.Image.FileName.ToLower());
				return FileSettings.AllowedImageExtensions.Contains(extension);
			})
			.WithMessage("Not Allowed Image Extension.")
			.When(x => x.Image is not null);
	}
}
