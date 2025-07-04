namespace FileManagement.Api.Contract.Common;

public class AllowedTypeContentValidator : AbstractValidator<IFormFile>
{
	public AllowedTypeContentValidator()
	{
		RuleFor(x => x)
			.Must((request, context) =>
			{
				// get file signature 
				// each content type has unique singnature ==> you can get it from the first 2 bytes
				// even if you change the original file exension  ex: from .png to .jpg the signature will return the [.png signature]

				BinaryReader binary = new(request.OpenReadStream());
				var bytes = binary.ReadBytes(2);
				var fileSequenceHex = BitConverter.ToString(bytes);

				foreach (var signature in FileSettings.BlockedSignatures)
					if (signature.Equals(fileSequenceHex, StringComparison.OrdinalIgnoreCase))
						return false;

				return true;
			})
			.WithMessage("Not Allowd Type Contnt")
			.When(x => x is not null);

	}
}
