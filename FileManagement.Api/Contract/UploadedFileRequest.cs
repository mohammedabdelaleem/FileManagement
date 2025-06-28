namespace FileManagement.Api.Contract;

public record UploadedFileRequest(
	IFormFile File
	);