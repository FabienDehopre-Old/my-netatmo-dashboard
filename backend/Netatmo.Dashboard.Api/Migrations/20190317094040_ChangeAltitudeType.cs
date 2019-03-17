using Microsoft.EntityFrameworkCore.Migrations;

namespace Netatmo.Dashboard.Api.Migrations
{
    public partial class ChangeAltitudeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Altitude",
                table: "Stations",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Altitude",
                table: "Stations",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
