using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data.Entities;
using PublishingHouse.Models;

namespace PublishingHouse.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{ }

	public DbSet<User> Users { get; set; }
	public DbSet<Author> Authors { get; set; }
	public DbSet<Gender> Genders { get; set; }
	public DbSet<City> Cities { get; set; }
	public DbSet<Country> Countries { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<ProductType> ProductTypes { get; set; }
	public DbSet<Publisher> Publishers { get; set; }
	public DbSet<ProductAuthor> ProductAuthors { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Many to Many Relationship Between Product and Author
		modelBuilder.Entity<ProductAuthor>()
			.HasOne(pa => pa.Product)
			.WithMany(p => p.ProductAuthors)
			.HasForeignKey(pa => pa.ProductId);

		modelBuilder.Entity<ProductAuthor>()
			.HasOne(pa => pa.Author)
			.WithMany(a => a.ProductAuthors)
			.HasForeignKey(pa => pa.AuthorId);

		modelBuilder.Entity<ProductAuthor>()
			.HasIndex(pa => new { pa.ProductId, pa.AuthorId })
			.IsUnique();

		// ProductType.Type is Unique
		modelBuilder.Entity<ProductType>()
		   .HasIndex(pt => pt.Type)
		   .IsUnique();

		// Gender.Title is Unique
		modelBuilder.Entity<Gender>()
	   .HasIndex(g => g.Title)
	   .IsUnique();
	}
}
