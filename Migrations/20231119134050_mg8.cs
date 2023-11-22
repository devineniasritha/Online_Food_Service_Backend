using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineFoodService.Migrations
{
    public partial class mg8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total_Cost",
                table: "Carts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Total_Cost",
                table: "Carts",
                type: "int",
                nullable: true);
        }
    }
}
