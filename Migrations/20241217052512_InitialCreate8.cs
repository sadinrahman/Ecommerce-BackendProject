using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { 1, "HotWheels" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "ProductId", "CategoryId", "Description", "Image", "Price", "Title", "stock" },
                values: new object[] { 1, 1, "he Hot Wheels Nissan GT-R R34 is a detailed 1:64 scale die-cast model, capturing the iconic design and performance of the legendary Japanese sports car. It features aggressive styling, signature features like quad headlights and a rear spoiler, often in various color schemes and special editions", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.amazon.in%2FHot-Wheels-Nissan-Skyline-Imports%2Fdp%2FB0DDHLX5B3&psig=AOvVaw3EoMvC6pCTZDa6eXw2gY_8&ust=1734499339117000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCKC409uHrooDFQAAAAAdAAAAABAE", 700m, "Nissan Gtr R34", 20 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 1);
        }
    }
}
