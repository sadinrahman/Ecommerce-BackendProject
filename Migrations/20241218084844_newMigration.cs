using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProject.Migrations
{
    /// <inheritdoc />
    public partial class newMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 18,
                column: "Password",
                value: "$2a$11$zcyY5liBidVUZo9EmLL2l./oFLutH7CVmBEaopI/d75T.XN3P7VSC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 18,
                column: "Password",
                value: "$2a$11$vvlubxF.m8pvTOqiKfrXO.UJwqZ0A34JLW.pi7m121gj/A/q1qh1i");
        }
    }
}
