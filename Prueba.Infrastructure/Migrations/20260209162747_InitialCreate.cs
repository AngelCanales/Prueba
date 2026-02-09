using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Prueba.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Identidad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Existencia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    OrdenId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<long>(type: "bigint", nullable: false),
                    Impuesto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.OrdenId);
                    table.ForeignKey(
                        name: "FK_Ordenes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleOrdenes",
                columns: table => new
                {
                    DetalleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenId = table.Column<long>(type: "bigint", nullable: false),
                    ProductoId = table.Column<long>(type: "bigint", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Impuesto = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleOrdenes", x => x.DetalleId);
                    table.ForeignKey(
                        name: "FK_DetalleOrdenes_Ordenes_OrdenId",
                        column: x => x.OrdenId,
                        principalTable: "Ordenes",
                        principalColumn: "OrdenId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleOrdenes_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "ClienteId", "Identidad", "Nombre" },
                values: new object[,]
                {
                    { 1L, "0801-1990-12345", "Juan Pérez" },
                    { 2L, "0801-1985-67890", "María González" },
                    { 3L, "0801-1992-11111", "Carlos Rodríguez" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "ProductoId", "Descripcion", "Existencia", "Nombre", "Precio" },
                values: new object[,]
                {
                    { 1L, "Laptop de alto rendimiento", 50, "Laptop Dell XPS 15", 1299.99m },
                    { 2L, "Mouse ergonómico inalámbrico", 150, "Mouse Logitech MX Master 3", 99.99m },
                    { 3L, "Teclado mecánico retroiluminado", 75, "Teclado Mecánico Keychron K2", 89.99m },
                    { 4L, "Monitor 4K UHD", 30, "Monitor LG 27\" 4K", 449.99m },
                    { 5L, "Webcam Full HD 1080p", 100, "Webcam Logitech C920", 79.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleOrden_OrdenId",
                table: "DetalleOrdenes",
                column: "OrdenId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleOrden_ProductoId",
                table: "DetalleOrdenes",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_ClienteId",
                table: "Ordenes",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleOrdenes");

            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
