using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotrSwap.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnusedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "VehiclePostings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "VehiclePostings",
                type: "TEXT",
                nullable: true);
        }
    }
}
