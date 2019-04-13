using Microsoft.EntityFrameworkCore.Migrations;

namespace Netatmo.Dashboard.Data.Migrations
{
    public partial class UpdateDataModelForLazyLoading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Devices",
                newName: "module_type");

            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "DashboardData",
                newName: "type");

            migrationBuilder.AlterColumn<decimal>(
                name: "WindStrength",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "GustStrength",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "module_type",
                table: "Devices",
                newName: "Discriminator");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "DashboardData",
                newName: "Discriminator");

            migrationBuilder.AlterColumn<int>(
                name: "WindStrength",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GustStrength",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
