using Microsoft.EntityFrameworkCore.Migrations;

namespace Netatmo.Dashboard.Api.Migrations
{
    public partial class AddUnitsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Units_FeelLike",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Units_PressureUnit",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Units_Unit",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Units_WindUnit",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Units_FeelLike",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Units_PressureUnit",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Units_Unit",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Units_WindUnit",
                table: "Users");
        }
    }
}
