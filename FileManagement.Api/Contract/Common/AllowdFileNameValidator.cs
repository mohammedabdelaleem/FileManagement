
namespace FileManagement.Api.Contract.Common;

public class AllowdFileNameValidator : AbstractValidator<string>
{
	public AllowdFileNameValidator()
	{
		RuleFor(x => x)
				.NotEmpty()
				.Matches(RegexPatterns.AllowedFileNameConvention)
				.WithMessage("Filename contains invalid characters.");

	}
}


/*
 namespace FileManager.Api.Contracts.Common;

public class FileNameValidator : AbstractValidator<IFormFile>
{
    public FileNameValidator()
    {
        RuleFor(x => x.FileName)
            .Matches("^[A-Za-z0-9_\\-.]*$")
            .WithMessage("Invalid file name")
            .When(x => x is not  null);
    }
}
 * */