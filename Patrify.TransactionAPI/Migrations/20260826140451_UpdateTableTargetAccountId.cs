using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patrify.TransactionAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableTargetAccountId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TargetAccountId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetAccountId",
                table: "Transactions");
        }
    }
}
