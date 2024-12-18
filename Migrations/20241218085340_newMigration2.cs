using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProject.Migrations
{
    /// <inheritdoc />
    public partial class newMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: 18);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "ConformPassword", "Email", "Password", "Role", "UserName" },
                values: new object[] { 18, null, "admin@gmail.com", "$2a$11$xYQK49W1KNnqGMVuT328aewZ2sAl6xnP3w/Loo/FSPZiYHmTlkDaS", "Admin", "admin" });
        }
    }
}
