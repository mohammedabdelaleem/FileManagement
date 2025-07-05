
namespace FileManagement.Api.Contract;

public class UploadManyFilesRequestValidator : AbstractValidator<UploadManyFilesRequest>
{
	public UploadManyFilesRequestValidator()
	{

		RuleForEach(x => x.Files)
			.SetValidator(new FileSizeValidator())
			.SetValidator(new BlockedSignaturesValidator())
			.ChildRules(file =>
			{
				file.RuleFor(f => f.FileName)
				.SetValidator(new AllowdFileNameValidator());
			});


	}
}
