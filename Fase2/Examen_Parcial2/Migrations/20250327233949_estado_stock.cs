using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen_Parcial2.Migrations
{
    /// <inheritdoc />
    public partial class estado_stock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "estado",
                table: "Stocks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "estado",
                table: "Stocks");
        }
    }
}
