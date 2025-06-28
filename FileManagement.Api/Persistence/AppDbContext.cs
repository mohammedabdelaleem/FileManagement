namespace FileManagement.Api.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<UploadedFile> Files { get; set; } = default!;


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 
		base.OnModelCreating(modelBuilder);
	}
}
