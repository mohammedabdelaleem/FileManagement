namespace FileManagement.Api.Contract.Common;

public class AllowedVideoSignatureValidator : AbstractValidator<IFormFile>
{
	public AllowedVideoSignatureValidator()
	{
		RuleFor(x => x)
			.Must((request, context) =>
			{
				var fileSignature = FileSettings.ExtractFileSignature(request, true);
				var allowedVideoSignatures = FileSettings.AllowedVideoSignatures;

				return (allowedVideoSignatures.Contains(fileSignature));
			})
			.WithMessage("Not Allowed Vidoe Content Type.")
			.When(x => x is not null);
	}
}
