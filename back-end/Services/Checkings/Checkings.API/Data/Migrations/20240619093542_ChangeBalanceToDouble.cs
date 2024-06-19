using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Checkings.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBalanceToDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "CheckingAccounts",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "CheckingAccounts",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
