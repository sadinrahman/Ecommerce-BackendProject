using BackendProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.AppdbContext
{
	public class AppDbContext:DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public DbSet<User> users { get; set; }
		public DbSet<Product> products { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.Property(x => x.Role)
				.HasDefaultValue("user");
			modelBuilder.Entity<Category>()
				.HasMany(x => x.products)
				.WithOne(r => r.category)
				.HasForeignKey(x => x.CategoryId);
			modelBuilder.Entity<Product>()
				.Property(pr => pr.Price).
				HasPrecision(18, 2);
			modelBuilder.Entity<Product>().HasData(
				new Product { ProductId = 1,Title="Nissan Gtr R34",Description= "he Hot Wheels Nissan GT-R R34 is a detailed 1:64 scale die-cast model, capturing the iconic design and performance of the legendary Japanese sports car. It features aggressive styling, signature features like quad headlights and a rear spoiler, often in various color schemes and special editions",Price=700,Image= "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.amazon.in%2FHot-Wheels-Nissan-Skyline-Imports%2Fdp%2FB0DDHLX5B3&psig=AOvVaw3EoMvC6pCTZDa6eXw2gY_8&ust=1734499339117000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCKC409uHrooDFQAAAAAdAAAAABAE",stock=20,CategoryId=1}
				);
			modelBuilder.Entity<Category>().HasData(
				new Category { CategoryId=1,Name="HotWheels"}
				);
		}
	}
}
