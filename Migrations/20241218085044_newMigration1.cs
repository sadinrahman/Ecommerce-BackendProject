using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProject.Migrations
{
    /// <inheritdoc />
    public partial class newMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 18,
                column: "Password",
                value: "$2a$11$xYQK49W1KNnqGMVuT328aewZ2sAl6xnP3w/Loo/FSPZiYHmTlkDaS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 18,
                column: "Password",
                value: "$2a$11$zcyY5liBidVUZo9EmLL2l./oFLutH7CVmBEaopI/d75T.XN3P7VSC");
        }
    }
}
