using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Savings.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendAccountModelWithConcreteTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavingAccounts");

            migrationBuilder.CreateTable(
                name: "HighYieldAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmailOwner = table.Column<string>(type: "text", nullable: false),
                    AccountNumber = table.Column<Guid>(type: "uuid", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    ClosedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InterestRate = table.Column<double>(type: "double precision", nullable: false),
                    AccrualPeriod = table.Column<string>(type: "text", nullable: false),
                    WithdrawalLimitsPerMonth = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighYieldAccounts", x => x.Id);
                    table.UniqueConstraint("AK_HighYieldAccounts_AccountNumber", x => x.AccountNumber);
                });

            migrationBuilder.CreateTable(
                name: "MoneyMarketAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmailOwner = table.Column<string>(type: "text", nullable: false),
                    AccountNumber = table.Column<Guid>(type: "uuid", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    ClosedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InterestRate = table.Column<double>(type: "double precision", nullable: false),
                    AccrualPeriod = table.Column<string>(type: "text", nullable: false),
                    WithdrawalLimitsPerMonth = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyMarketAccounts", x => x.Id);
                    table.UniqueConstraint("AK_MoneyMarketAccounts_AccountNumber", x => x.AccountNumber);
                });

            migrationBuilder.CreateTable(
                name: "RegularAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmailOwner = table.Column<string>(type: "text", nullable: false),
                    AccountNumber = table.Column<Guid>(type: "uuid", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    ClosedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InterestRate = table.Column<double>(type: "double precision", nullable: false),
                    AccrualPeriod = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularAccounts", x => x.Id);
                    table.UniqueConstraint("AK_RegularAccounts_AccountNumber", x => x.AccountNumber);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HighYieldAccounts");

            migrationBuilder.DropTable(
                name: "MoneyMarketAccounts");

            migrationBuilder.DropTable(
                name: "RegularAccounts");

            migrationBuilder.CreateTable(
                name: "SavingAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountNumber = table.Column<Guid>(type: "uuid", nullable: false),
                    AccrualPeriod = table.Column<string>(type: "text", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    ClosedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    EmailOwner = table.Column<string>(type: "text", nullable: false),
                    InterestRate = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingAccounts", x => x.Id);
                    table.UniqueConstraint("AK_SavingAccounts_AccountNumber", x => x.AccountNumber);
                });
        }
    }
}
