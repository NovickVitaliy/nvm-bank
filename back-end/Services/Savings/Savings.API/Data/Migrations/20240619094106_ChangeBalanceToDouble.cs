using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Savings.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBalanceToDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "RegularAccounts",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "MoneyMarketAccounts",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,0)");

            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "HighYieldAccounts",
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
                table: "RegularAccounts",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "MoneyMarketAccounts",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "HighYieldAccounts",
                type: "numeric(20,0)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
