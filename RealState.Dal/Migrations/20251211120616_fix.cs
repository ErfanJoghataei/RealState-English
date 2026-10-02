using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealState.Dal.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Category was created by the earlier property migration.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No schema change in Up.
        }
    }
}
