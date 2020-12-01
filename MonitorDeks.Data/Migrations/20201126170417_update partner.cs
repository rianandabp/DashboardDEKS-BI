using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitorDeks.Data.Migrations
{
    public partial class updatepartner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CaratakerPhoneNumber",
                table: "Partners",
                newName: "CaretakerPhoneNumber");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramId",
                table: "Partners",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CaretakerPhoneNumber",
                table: "Partners",
                newName: "CaratakerPhoneNumber");

            migrationBuilder.AlterColumn<string>(
                name: "ProgramId",
                table: "Partners",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
