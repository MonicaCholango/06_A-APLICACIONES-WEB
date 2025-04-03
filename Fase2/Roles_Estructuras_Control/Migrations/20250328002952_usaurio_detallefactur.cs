using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roles_Estructuras_Control.Migrations
{
    /// <inheritdoc />
    public partial class usaurio_detallefactur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "DetalleFactura",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuariosModelId",
                table: "DetalleFactura",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_UsuariosModelId",
                table: "DetalleFactura",
                column: "UsuariosModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleFactura_AspNetUsers_UsuariosModelId",
                table: "DetalleFactura",
                column: "UsuariosModelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleFactura_AspNetUsers_UsuariosModelId",
                table: "DetalleFactura");

            migrationBuilder.DropIndex(
                name: "IX_DetalleFactura_UsuariosModelId",
                table: "DetalleFactura");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "DetalleFactura");

            migrationBuilder.DropColumn(
                name: "UsuariosModelId",
                table: "DetalleFactura");
        }
    }
}
