using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealState.Dal.Migrations
{
    /// <inheritdoc />
    public partial class addcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "sellVillas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "sellApartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "rentVillas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "rentApartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "lands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Gaurdens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "sellVillas");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "sellApartments");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "rentVillas");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "rentApartments");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "lands");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Gaurdens");
        }
    }
}
