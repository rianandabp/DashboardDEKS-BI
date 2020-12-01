using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitorDeks.Data.Migrations
{
    public partial class updatepartnerrrrr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Partners",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Partners");
        }
    }
}
