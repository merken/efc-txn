using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Register.Data.Migrations
{
    public partial class Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Shipments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Account",
                table: "BankTransfers",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "BankTransfers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransferDateTime",
                table: "BankTransfers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "Account",
                table: "BankTransfers");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "BankTransfers");

            migrationBuilder.DropColumn(
                name: "TransferDateTime",
                table: "BankTransfers");
        }
    }
}
