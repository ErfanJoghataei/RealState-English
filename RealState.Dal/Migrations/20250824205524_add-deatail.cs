using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealState.Dal.Migrations
{
    /// <inheritdoc />
    public partial class adddeatail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "sellVillas",
                newName: "province");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "sellApartments",
                newName: "province");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "sellVillas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "neighborhood",
                table: "sellVillas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "sellApartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "neighborhood",
                table: "sellApartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "sellVillas");

            migrationBuilder.DropColumn(
                name: "neighborhood",
                table: "sellVillas");

            migrationBuilder.DropColumn(
                name: "City",
                table: "sellApartments");

            migrationBuilder.DropColumn(
                name: "neighborhood",
                table: "sellApartments");

            migrationBuilder.RenameColumn(
                name: "province",
                table: "sellVillas",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "province",
                table: "sellApartments",
                newName: "Location");
        }
    }
}
