using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealState.Dal.Migrations
{
    /// <inheritdoc />
    public partial class adduserdetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "JoneDate",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JoneDate",
                table: "Users");
        }
    }
}
