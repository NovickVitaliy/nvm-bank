using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Checkings.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddClosedOnPropertyToCheckingAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedOn",
                table: "CheckingAccounts",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosedOn",
                table: "CheckingAccounts");
        }
    }
}
