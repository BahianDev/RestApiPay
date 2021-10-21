using Microsoft.EntityFrameworkCore.Migrations;

namespace deCasa.Migrations
{
    public partial class removedefaultvalueid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "3",
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
