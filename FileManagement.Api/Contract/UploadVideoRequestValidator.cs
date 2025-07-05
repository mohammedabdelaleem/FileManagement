namespace FileManagement.Api.Contract;

public class UploadVideoRequestValidator : AbstractValidator<UploadVideoRequest>
{
	public UploadVideoRequestValidator()
	{
		RuleFor(x => x.Video.FileName)
			.SetValidator(new AllowdFileNameValidator());


		RuleFor(x => x.Video)
			.SetValidator(new AllowedVideoSignatureValidator());
	}
}
