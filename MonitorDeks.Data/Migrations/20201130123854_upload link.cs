using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitorDeks.Data.Migrations
{
    public partial class uploadlink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UploadLink",
                table: "Partners",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadLink",
                table: "Partners");
        }
    }
}
