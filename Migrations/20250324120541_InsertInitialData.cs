using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transferencias.Migrations
{
    /// <inheritdoc />
    public partial class InsertInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TargetAccount",
                table: "Operation",
                newName: "TargetAccountId");

            migrationBuilder.RenameColumn(
                name: "OriginAccount",
                table: "Operation",
                newName: "OriginAccountId");

            migrationBuilder.AddColumn<string>(
                name: "Doc",
                table: "Account",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Account",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operation_OriginAccountId",
                table: "Operation",
                column: "OriginAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Operation_TargetAccountId",
                table: "Operation",
                column: "TargetAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operation_Account_OriginAccountId",
                table: "Operation",
                column: "OriginAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operation_Account_TargetAccountId",
                table: "Operation",
                column: "TargetAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operation_Account_OriginAccountId",
                table: "Operation");

            migrationBuilder.DropForeignKey(
                name: "FK_Operation_Account_TargetAccountId",
                table: "Operation");

            migrationBuilder.DropIndex(
                name: "IX_Operation_OriginAccountId",
                table: "Operation");

            migrationBuilder.DropIndex(
                name: "IX_Operation_TargetAccountId",
                table: "Operation");

            migrationBuilder.DropColumn(
                name: "Doc",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "TargetAccountId",
                table: "Operation",
                newName: "TargetAccount");

            migrationBuilder.RenameColumn(
                name: "OriginAccountId",
                table: "Operation",
                newName: "OriginAccount");
        }
    }
}
