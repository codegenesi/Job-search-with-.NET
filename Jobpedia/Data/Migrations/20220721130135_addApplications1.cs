using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobpedia.Data.Migrations
{
    public partial class addApplications1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PositionApplied",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionApplied",
                table: "Applications");
        }
    }
}
