

namespace FileManagement.Api.Persistence.EntititesConfigurations;

public class UploadedFileConfigurations : IEntityTypeConfiguration<UploadedFile>
{
	public void Configure(EntityTypeBuilder<UploadedFile> builder)
	{
		builder.Property(x => x.Id)
			.IsRequired();

		builder.Property(x => x.FileName).HasMaxLength(255);
		builder.Property(x => x.StoredFileName).HasMaxLength(255);
		builder.Property(x => x.ContentType).HasMaxLength(50);
		builder.Property(x => x.FileExtension).HasMaxLength(10);

	}
}
