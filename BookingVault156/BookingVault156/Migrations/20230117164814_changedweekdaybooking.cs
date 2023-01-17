using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingVault156.Migrations
{
    public partial class changedweekdaybooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookedDate",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "BookedDay",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookedWeek",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookedDay",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookedWeek",
                table: "Bookings");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookedDate",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
