using Microsoft.EntityFrameworkCore.Migrations;

namespace Netatmo.Dashboard.Data.Migrations
{
    public partial class SetDecimalType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Stations",
                type: "decimal(10,7)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Stations",
                type: "decimal(9,7)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "BatteryPercent",
                table: "Devices",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "WindStrength",
                table: "DashboardData",
                type: "decimal(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "GustStrength",
                table: "DashboardData",
                type: "decimal(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Sum24H",
                table: "DashboardData",
                type: "decimal(5,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Sum1H",
                table: "DashboardData",
                type: "decimal(5,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Rain",
                table: "DashboardData",
                type: "decimal(5,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OutdoorDashboardData_TemperatureMin",
                table: "DashboardData",
                type: "decimal(4,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OutdoorDashboardData_TemperatureMax",
                table: "DashboardData",
                type: "decimal(4,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OutdoorDashboardData_Temperature",
                table: "DashboardData",
                type: "decimal(4,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MainDashboardData_TemperatureMin",
                table: "DashboardData",
                type: "decimal(4,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MainDashboardData_TemperatureMax",
                table: "DashboardData",
                type: "decimal(4,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MainDashboardData_Temperature",
                table: "DashboardData",
                type: "decimal(4,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Pressure",
                table: "DashboardData",
                type: "decimal(5,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AbsolutePressure",
                table: "DashboardData",
                type: "decimal(5,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TemperatureMin",
                table: "DashboardData",
                type: "decimal(4,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TemperatureMax",
                table: "DashboardData",
                type: "decimal(4,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Temperature",
                table: "DashboardData",
                type: "decimal(4,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Stations",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,7)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Stations",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,7)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BatteryPercent",
                table: "Devices",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "WindStrength",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "GustStrength",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Sum24H",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Sum1H",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Rain",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OutdoorDashboardData_TemperatureMin",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OutdoorDashboardData_TemperatureMax",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OutdoorDashboardData_Temperature",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MainDashboardData_TemperatureMin",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MainDashboardData_TemperatureMax",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MainDashboardData_Temperature",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Pressure",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AbsolutePressure",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TemperatureMin",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TemperatureMax",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Temperature",
                table: "DashboardData",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,1)",
                oldNullable: true);
        }
    }
}
