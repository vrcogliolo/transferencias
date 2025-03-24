using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transferencias.Migrations
{
    /// <inheritdoc />
    public partial class InsertInitialAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Account",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Doc",
                table: "Account",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
            
            migrationBuilder.InsertData(
                table: "Account",
                columns: ["Name", "Doc", "Balance"],
                values: new object[,]
                {
                    { "Victor", "131346", 30000000 },
                    { "Juan", "131347", 10000000 },
                    { "Perez", "131348", 15000000 }
                });
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Account",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Doc",
                table: "Account",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
            
            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Doc",
                keyValues: ["131346", "131347", "131348"]);
        }
    }
}
