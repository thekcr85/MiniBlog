using Microsoft.EntityFrameworkCore;
using MiniBlogApp.Models;

namespace MiniBlogApp.Data;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
	public DbSet<Author> Authors => Set<Author>();
	public DbSet<Comment> Comments => Set<Comment>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Author>(entity =>
		{
			entity.Property(a => a.FirstName).IsRequired().HasMaxLength(50);
			entity.Property(a => a.LastName).IsRequired().HasMaxLength(50);
			entity.Property(a => a.Email).IsRequired().HasMaxLength(50);
			entity.Property(a => a.Biography).HasMaxLength(2000);
			entity.Property(a => a.CreatedAt).IsRequired();

			entity.HasMany(a => a.BlogPosts)
				.WithOne(p => p.Author)
				.HasForeignKey(p => p.AuthorId)
				.OnDelete(DeleteBehavior.Restrict);
		});

		modelBuilder.Entity<BlogPost>(entity =>
		{
			entity.Property(p => p.Title).IsRequired().HasMaxLength(100);
			entity.Property(p => p.Summary).IsRequired().HasMaxLength(500);
			entity.Property(p => p.Content).IsRequired();
			entity.Property(p => p.CreatedAt).IsRequired();
			entity.Property(p => p.AuthorId).IsRequired();

			entity.HasMany(p => p.Comments)
				.WithOne(c => c.BlogPost)
				.HasForeignKey(c => c.BlogPostId)
				.OnDelete(DeleteBehavior.Cascade);
		});

		modelBuilder.Entity<Comment>(entity =>
		{
			entity.Property(c => c.Content).IsRequired().HasMaxLength(1000);
			entity.Property(c => c.AuthorName).IsRequired().HasMaxLength(50);
			entity.Property(c => c.AuthorEmail).HasMaxLength(50);
			entity.Property(c => c.CreatedAt).IsRequired();
			entity.Property(c => c.BlogPostId).IsRequired();
		});
	}
}
