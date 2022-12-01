using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Checklist.Migrations
{
    /// <inheritdoc />
    public partial class updateAlert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Alert",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alert",
                table: "Assignments");
        }
    }
}
