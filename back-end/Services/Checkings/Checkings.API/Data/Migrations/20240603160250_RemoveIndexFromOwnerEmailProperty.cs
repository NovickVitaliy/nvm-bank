using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Checkings.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIndexFromOwnerEmailProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_CheckingAccounts_OwnerEmail",
                table: "CheckingAccounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_CheckingAccounts_OwnerEmail",
                table: "CheckingAccounts",
                column: "OwnerEmail");
        }
    }
}
