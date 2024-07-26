using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricityBillingSystemDemo.Migrations
{
    public partial class customermodelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetOTP",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PasswordResetOTPCreationTime",
                table: "Customers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordResetOTP",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetOTPCreationTime",
                table: "Customers",
                type: "datetime2",
                nullable: true);
        }
    }
}
