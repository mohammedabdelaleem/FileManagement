namespace FileManagement.Api.Contract;

public class UploadFileRequestValidator : AbstractValidator<UploadedFileRequest>
{
	public UploadFileRequestValidator()
	{
		RuleFor(x => x.File)
			.Must((request, context) => request.File.Length <= FileSettings.MaxFileSizeInBytes)
			.WithMessage($"Max File Size Is {FileSettings.MaxFileSizeInMB} MB.")
			.When(x => x.File is not null);
	}
}
