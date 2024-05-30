﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsMainPhoneColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "PhoneNumbers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "PhoneNumbers");
        }
    }
}
