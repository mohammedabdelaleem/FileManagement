namespace FileManagement.Api.Entity;

public sealed class UploadedFile
{
	public Guid Id { get; set; } = Guid.CreateVersion7();

	// the original filename
	public string FileName { get; set; } = string.Empty; // my-image.png

	// the fake file name , saved at server storage		
	public string StoredFileName { get; set; } = string.Empty;// koXsFx.Kox

	public string ContentType { get; set; } = string.Empty; // we deal with files we need to know what its type?

	public string FileExtension { get; set; } = string.Empty;

}
