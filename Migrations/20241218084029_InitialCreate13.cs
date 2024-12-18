using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$gEVUCB/UoIurY/OdZ./B6O8rt7TsvHiaS3wBuUa6LkTV2xZ0i/LNC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$T7cwEPBzFpZInEoIVR3DiOQ7zWNb3gjGV8uX1a9JLT7c78PFSIY42");
        }
    }
}
