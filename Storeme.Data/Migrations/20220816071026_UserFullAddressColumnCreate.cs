using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Storeme.Data.Migrations
{
    public partial class UserFullAddressColumnCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullAddress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullAddress",
                table: "AspNetUsers");
        }
    }
}
