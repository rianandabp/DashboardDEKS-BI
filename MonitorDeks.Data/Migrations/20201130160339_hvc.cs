using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitorDeks.Data.Migrations
{
    public partial class hvc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskDone",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TaskLate",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskDone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TaskLate",
                table: "Users");
        }
    }
}
