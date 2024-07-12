using Microsoft.EntityFrameworkCore.Migrations;

namespace GetCuredProject.Data.Migrations
{
    public partial class addingimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Doctor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Doctor");
        }
    }
}
