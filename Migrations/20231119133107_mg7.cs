using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineFoodService.Migrations
{
    public partial class mg7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Total_Cost",
                table: "Carts",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total_Cost",
                table: "Carts");
        }
    }
}
