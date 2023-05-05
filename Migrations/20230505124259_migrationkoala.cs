using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaMania.Migrations
{
    /// <inheritdoc />
    public partial class migrationkoala : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryTime",
                table: "Pizzas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryTime",
                table: "Pizzas",
                type: "text",
                nullable: true);
        }
    }
}
