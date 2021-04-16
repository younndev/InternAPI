using Microsoft.EntityFrameworkCore.Migrations;

namespace InternAPI.Migrations
{
    public partial class CompanyIdAdded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Companies_CompanyId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_CompanyId",
                table: "Student");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Student",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Student_CompanyId",
                table: "Student",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Companies_CompanyId",
                table: "Student",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
