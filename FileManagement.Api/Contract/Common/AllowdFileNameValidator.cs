
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
