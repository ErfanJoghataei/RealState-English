using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealState.Dal.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gaurdens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    FloorArea = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    neighborhood = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gaurdens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    FloorArea = table.Column<int>(type: "int", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    neighborhood = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Servis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rentApartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstMoney = table.Column<int>(type: "int", nullable: false),
                    MoneyRent = table.Column<int>(type: "int", nullable: false),
                    FloorArea = table.Column<int>(type: "int", nullable: false),
                    DateOfBuild = table.Column<DateTime>(type: "datetime2", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    neighborhood = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Servis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TheFloor = table.Column<int>(type: "int", nullable: false),
                    Elevator = table.Column<bool>(type: "bit", nullable: false),
                    RoomCount = table.Column<int>(type: "int", nullable: false),
                    Parking = table.Column<bool>(type: "bit", nullable: false),
                    warehouse = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rentApartments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rentVillas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    FloorArea = table.Column<int>(type: "int", nullable: false),
                    DateOfBuild = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Servis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Elevator = table.Column<bool>(type: "bit", nullable: false),
                    RoomCount = table.Column<int>(type: "int", nullable: false),
                    Parking = table.Column<bool>(type: "bit", nullable: false),
                    warehouse = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rentVillas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sellApartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    FloorArea = table.Column<int>(type: "int", nullable: false),
                    DateOfBuild = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Servis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TheFloor = table.Column<int>(type: "int", nullable: false),
                    Elevator = table.Column<bool>(type: "bit", nullable: false),
                    RoomCount = table.Column<int>(type: "int", nullable: false),
                    Parking = table.Column<bool>(type: "bit", nullable: false),
                    warehouse = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sellApartments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sellVillas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    FloorArea = table.Column<int>(type: "int", nullable: false),
                    DateOfBuild = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Servis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Elevator = table.Column<bool>(type: "bit", nullable: false),
                    RoomCount = table.Column<int>(type: "int", nullable: false),
                    Parking = table.Column<bool>(type: "bit", nullable: false),
                    warehouse = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sellVillas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Gaurdens");

            migrationBuilder.DropTable(
                name: "lands");

            migrationBuilder.DropTable(
                name: "rentApartments");

            migrationBuilder.DropTable(
                name: "rentVillas");

            migrationBuilder.DropTable(
                name: "sellApartments");

            migrationBuilder.DropTable(
                name: "sellVillas");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
