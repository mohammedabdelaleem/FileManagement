namespace FileManagement.Api.Contract.Common;

public class BlockedSignaturesValidator : AbstractValidator<IFormFile>
{
	public BlockedSignaturesValidator()
	{
		RuleFor(x => x)
			.Must((request, context) =>
			{
				
				var fileSequenceHex = FileSettings.ExtractFileSignature(request);

				foreach (var signature in FileSettings.BlockedSignatures)
					if (signature.Equals(fileSequenceHex, StringComparison.OrdinalIgnoreCase))
						return false;

				return true;
			})
			.WithMessage("Not Allowd Type Contnt")
			.When(x => x is not null);

	}
}
