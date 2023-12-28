using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace week05_PTW.Migrations
{
    public partial class DBFirst5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone_Number",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone_Number",
                table: "AspNetUsers");
        }
    }
}
