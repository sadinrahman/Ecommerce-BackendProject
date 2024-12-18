using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "ConformPassword", "Email", "Password", "Role", "UserName" },
                values: new object[] { 18, null, "admin@gmail.com", "$2a$11$vvlubxF.m8pvTOqiKfrXO.UJwqZ0A34JLW.pi7m121gj/A/q1qh1i", "Admin", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "ConformPassword", "Email", "Password", "Role", "UserName" },
                values: new object[] { 3, null, "admin@gmail.com", "$2a$11$gEVUCB/UoIurY/OdZ./B6O8rt7TsvHiaS3wBuUa6LkTV2xZ0i/LNC", "Admin", "admin" });
        }
    }
}
