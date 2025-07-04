namespace FileManagement.Api.Contract;

public record UploadManyFilesRequest
(
	IFormFileCollection Files	
);