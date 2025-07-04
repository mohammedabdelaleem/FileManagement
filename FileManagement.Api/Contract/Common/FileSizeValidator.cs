namespace FileManagement.Api.Contract.Common;

public class FileSizeValidator : AbstractValidator<IFormFile>
{
	public FileSizeValidator()
	{

		RuleFor(x => x)
			.Must((request, context) => request.Length <= FileSettings.MaxFileSizeInBytes)
			.WithMessage($"Max File Size Is {FileSettings.MaxFileSizeInMB} MB.")
			.When(x => x is not null);

	}
}
