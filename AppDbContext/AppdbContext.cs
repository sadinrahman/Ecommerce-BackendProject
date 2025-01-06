using BackendProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.AppdbContext
{
	public class AppDbContext:DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base	(options) { }
		public DbSet<User> users { get; set; }
		public DbSet<Product> products { get; set; }
		public DbSet<Category> Category { get; set; }
		public DbSet<Cart>	carts { get; set; }
		public DbSet<CartItems> cartItems { get; set; }
		public DbSet<WishList> wishList { get; set; }

		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItems> OrderItems { get; set; }
		public DbSet<Address> Addresses { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()  
				.Property(x => x.Role)
				.HasDefaultValue("user");
			modelBuilder.Entity<User>()
				.Property(x => x.IsBlocked)
				.HasDefaultValue("false");
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
			
			modelBuilder.Entity<User>()
				.HasOne(x => x.Cart)
				.WithOne(y => y.User)
				.HasForeignKey<Cart>(x => x.UserId);
			modelBuilder.Entity<Cart>()
				.HasMany(q => q.cartitems)
				.WithOne(w => w.Cart)
				.HasForeignKey(i => i.CartId);
			modelBuilder.Entity<CartItems>()
				.HasOne(f=>f.Product)
				.WithMany(o=>o.CartItems)
				.HasForeignKey(i => i.ProductId);
			modelBuilder.Entity<WishList>()
				.HasOne(x => x.users)
				.WithMany(w => w.WishList)
				.HasForeignKey(e => e.UserId);
			modelBuilder.Entity<WishList>()
				.HasOne(x => x.products)
				.WithMany()
				.HasForeignKey(e => e.ProductId);
			modelBuilder.Entity<Order>()
				.HasOne(x=>x.User)
				.WithMany(o=>o.Orders)
				.HasForeignKey(e => e.UserId);
			modelBuilder.Entity<Order>()
				.Property(x => x.Status)
				.HasDefaultValue("placed"); 
			modelBuilder.Entity<OrderItems>()
				.HasOne(p => p.Order)
				.WithMany(c => c.OrderItems)
				.HasForeignKey(d => d.OrderId);
			modelBuilder.Entity<OrderItems>()
				.HasOne(x => x.Product)
				.WithMany()
				.HasForeignKey(p => p.productId);
			modelBuilder.Entity<Address>()
				.HasOne(a => a.User)
				.WithMany(u => u.Addresses)
				.HasForeignKey(u => u.UserId);
			modelBuilder.Entity<Order>()
				.HasOne(o => o.Address)
				.WithMany(a => a.Orders)
				.HasForeignKey(u => u.AddressId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
