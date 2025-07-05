namespace FileManagement.Api.Contract;

public record FileResponse
	(
	byte[] FileContent,
	string ContentType,
	string FileName
	);