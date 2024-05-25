using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakePhoneNumberToBeUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_PhoneNumbers_Number",
                table: "PhoneNumbers",
                column: "Number");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_PhoneNumbers_Number",
                table: "PhoneNumbers");
        }
    }
}
