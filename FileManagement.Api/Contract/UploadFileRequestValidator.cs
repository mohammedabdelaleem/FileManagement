namespace FileManagement.Api.Contract;

public class UploadFileRequestValidator : AbstractValidator<UploadedFileRequest>
{
	public UploadFileRequestValidator()
	{

		RuleFor(x => x.File.FileName)
			.SetValidator(new AllowdFileNameValidator());


		RuleFor(x=>x.File)
			//.SetValidator(new FileSizeValidator())
			.SetValidator(new BlockedSignaturesValidator());
	}
}
	