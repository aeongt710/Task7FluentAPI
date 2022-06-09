using Microsoft.EntityFrameworkCore.Migrations;

namespace Task7FluentAPI.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderItems",
                newName: "Quanity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quanity",
                table: "OrderItems",
                newName: "Price");
        }
    }
}
