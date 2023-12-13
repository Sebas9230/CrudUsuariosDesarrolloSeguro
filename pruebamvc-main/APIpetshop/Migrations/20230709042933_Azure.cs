using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIpetshop.Migrations
{
    /// <inheritdoc />
    public partial class Azure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clienteCompras",
                columns: table => new
                {
                    idCompra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigoCompra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nombreCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    valorCompra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cedula = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clienteCompras", x => x.idCompra);
                });

            migrationBuilder.CreateTable(
                name: "contactos",
                columns: table => new
                {
                    idContacto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactos", x => x.idContacto);
                });

            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    idProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    picture = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos", x => x.idProducto);
                });

            migrationBuilder.InsertData(
                table: "clienteCompras",
                columns: new[] { "idCompra", "cedula", "codigoCompra", "nombreCliente", "valorCompra" },
                values: new object[,]
                {
                    { 101, "1002858874", "132789", "Sebastian Andrade", "230" },
                    { 102, "1701286291", "146321", "Samantha Caicedo", "189" },
                    { 103, "1700052132", "123579", "Darwin Hidrobo", "365" },
                    { 104, "1702858866", "965412", "Kate Dittmer", "210" },
                    { 105, "1742605671", "216546", "Juan Rosales", "195" }
                });

            migrationBuilder.InsertData(
                table: "productos",
                columns: new[] { "idProducto", "codeNumber", "name", "picture", "price", "quantity" },
                values: new object[,]
                {
                    { 1001, "watch001", "Cool Watch", "watch.png", "99", "1" },
                    { 1002, "watch002", "Diving Watch", "divingwatch.png", "89", "1" },
                    { 1003, "watch002", "Dress Watch", "dresswatch.png", "85", "1" },
                    { 1004, "watch004", "Military Watch", "militarywatch.png", "99", "1" },
                    { 1005, "watch004", "Wrist Watch", "wristwatch.png", "85", "1" },
                    { 1006, "watch004", "Army Watch", "awatch.png", "99", "1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clienteCompras");

            migrationBuilder.DropTable(
                name: "contactos");

            migrationBuilder.DropTable(
                name: "productos");
        }
    }
}
