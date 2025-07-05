using Azure.Core;

namespace FileManagement.Api.Settings;

public static class FileSettings
{
	public const int MaxFileSizeInMB = 1;
	public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
	public static readonly string[] BlockedSignatures = ["4D-5A", "2F-2A", "D0-CF", "25-50"];

	public static readonly string[] AllowedImageExtensions = [".png", ".jpg", ".jpeg"];

	public static readonly string[] AllowedVideoSignatures =
	[
	"00-00-00-18", // MP4 (ftyp major brand, may vary like ftypisom)
    "52-49-46-46", // AVI ("RIFF")
    "1A-45-DF-A3", // MKV / WebM (EBML)
    "46-4C-56",    // FLV ("FLV")
    "30-26-B2-75", // WMV/ASF
    "00-00-01-BA", // MPEG-PS
    "00-00-01-B3"  // MPEG-1
];


	public static string ExtractFileSignature(IFormFile file, bool videoStream=false)
	{
		// get file signature 
		// each content type has unique singnature ==> you can get it from the first 2 bytes
		// even if you change the original file exension  ex: from .png to .jpg the signature will return the [.png signature]

		BinaryReader binary = new(file.OpenReadStream());
		var bytes = binary.ReadBytes(videoStream ? 4 : 2);
		var fileSequenceHex = BitConverter.ToString(bytes);

		return fileSequenceHex;
	}
}
