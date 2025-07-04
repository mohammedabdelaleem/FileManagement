
namespace FileManagement.Api.Contract;

public class UploadManyFilesRequestValidator : AbstractValidator<UploadManyFilesRequest>
{
	public UploadManyFilesRequestValidator()
	{

		RuleForEach(x => x.Files)
			.ChildRules(file =>
			{
				file.RuleFor(f => f.FileName)
				.SetValidator(new AllowdFileNameValidator());
			});

		RuleForEach(x => x.Files)
			.SetValidator(new FileSizeValidator());	

		RuleForEach(x => x.Files)
			.SetValidator(new BlockedSignaturesValidator());

	}
}
