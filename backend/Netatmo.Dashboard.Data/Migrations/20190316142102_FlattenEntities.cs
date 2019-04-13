using Microsoft.EntityFrameworkCore.Migrations;

namespace Netatmo.Dashboard.Data.Migrations
{
    public partial class FlattenEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location_Country",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "Pressure_Trend",
                table: "DashboardData");

            migrationBuilder.DropColumn(
                name: "Temperature_Trend",
                table: "DashboardData");

            migrationBuilder.DropColumn(
                name: "TemperatureData_Temperature_Trend",
                table: "DashboardData");

            migrationBuilder.DropColumn(
                name: "TemperatureData_Temperature_Trend1",
                table: "DashboardData");

            migrationBuilder.RenameColumn(
                name: "Units_WindUnit",
                table: "Users",
                newName: "WindUnit");

            migrationBuilder.RenameColumn(
                name: "Units_Unit",
                table: "Users",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "Units_PressureUnit",
                table: "Users",
                newName: "PressureUnit");

            migrationBuilder.RenameColumn(
                name: "Units_FeelLike",
                table: "Users",
                newName: "FeelLike");

            migrationBuilder.RenameColumn(
                name: "Location_Timezone",
                table: "Stations",
                newName: "Timezone");

            migrationBuilder.RenameColumn(
                name: "Location_StaticMap",
                table: "Stations",
                newName: "StaticMap");

            migrationBuilder.RenameColumn(
                name: "Location_City",
                table: "Stations",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "Location_Altitude",
                table: "Stations",
                newName: "Altitude");

            migrationBuilder.RenameColumn(
                name: "Location_GeoLocation_Longitude",
                table: "Stations",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "Location_GeoLocation_Latitude",
                table: "Stations",
                newName: "Latitude");

            migrationBuilder.RenameColumn(
                name: "Battery_Vp",
                table: "Devices",
                newName: "BatteryVp");

            migrationBuilder.RenameColumn(
                name: "Battery_Percent",
                table: "Devices",
                newName: "BatteryPercent");

            migrationBuilder.RenameColumn(
                name: "TemperatureMinMax_Temperature_Min_Value1",
                table: "DashboardData",
                newName: "OutdoorDashboardData_TemperatureMin");

            migrationBuilder.RenameColumn(
                name: "TemperatureMinMax_Temperature_Min_Timestamp1",
                table: "DashboardData",
                newName: "OutdoorDashboardData_TemperatureMinTimestamp");

            migrationBuilder.RenameColumn(
                name: "TemperatureMinMax_Temperature_Min_Value",
                table: "DashboardData",
                newName: "OutdoorDashboardData_TemperatureMax");

            migrationBuilder.RenameColumn(
                name: "TemperatureMinMax_Temperature_Min_Timestamp",
                table: "DashboardData",
                newName: "OutdoorDashboardData_TemperatureMaxTimestamp");

            migrationBuilder.RenameColumn(
                name: "Temperature_Min_Value",
                table: "DashboardData",
                newName: "OutdoorDashboardData_Temperature");

            migrationBuilder.RenameColumn(
                name: "Temperature_Min_Timestamp",
                table: "DashboardData",
                newName: "MainDashboardData_TemperatureMinTimestamp");

            migrationBuilder.RenameColumn(
                name: "TemperatureMinMax_Temperature_Max_Value1",
                table: "DashboardData",
                newName: "MainDashboardData_TemperatureMin");

            migrationBuilder.RenameColumn(
                name: "TemperatureMinMax_Temperature_Max_Timestamp1",
                table: "DashboardData",
                newName: "MainDashboardData_TemperatureMaxTimestamp");

            migrationBuilder.RenameColumn(
                name: "TemperatureMinMax_Temperature_Max_Value",
                table: "DashboardData",
                newName: "MainDashboardData_TemperatureMax");

            migrationBuilder.RenameColumn(
                name: "TemperatureMinMax_Temperature_Max_Timestamp",
                table: "DashboardData",
                newName: "TemperatureMinTimestamp");

            migrationBuilder.RenameColumn(
                name: "Temperature_Max_Value",
                table: "DashboardData",
                newName: "MainDashboardData_Temperature");

            migrationBuilder.RenameColumn(
                name: "Temperature_Max_Timestamp",
                table: "DashboardData",
                newName: "TemperatureMaxTimestamp");

            migrationBuilder.RenameColumn(
                name: "TemperatureData_Temperature_Current1",
                table: "DashboardData",
                newName: "Pressure");

            migrationBuilder.RenameColumn(
                name: "TemperatureData_Temperature_Current",
                table: "DashboardData",
                newName: "AbsolutePressure");

            migrationBuilder.RenameColumn(
                name: "Temperature_Current",
                table: "DashboardData",
                newName: "TemperatureMin");

            migrationBuilder.RenameColumn(
                name: "Pressure_Value",
                table: "DashboardData",
                newName: "TemperatureMax");

            migrationBuilder.RenameColumn(
                name: "Pressure_Absolute",
                table: "DashboardData",
                newName: "Temperature");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Stations",
                fixedLength: true,
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TemperatureTrend",
                table: "DashboardData",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PressureTrend",
                table: "DashboardData",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MainDashboardData_TemperatureTrend",
                table: "DashboardData",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OutdoorDashboardData_TemperatureTrend",
                table: "DashboardData",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "TemperatureTrend",
                table: "DashboardData");

            migrationBuilder.DropColumn(
                name: "PressureTrend",
                table: "DashboardData");

            migrationBuilder.DropColumn(
                name: "MainDashboardData_TemperatureTrend",
                table: "DashboardData");

            migrationBuilder.DropColumn(
                name: "OutdoorDashboardData_TemperatureTrend",
                table: "DashboardData");

            migrationBuilder.RenameColumn(
                name: "WindUnit",
                table: "Users",
                newName: "Units_WindUnit");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "Users",
                newName: "Units_Unit");

            migrationBuilder.RenameColumn(
                name: "PressureUnit",
                table: "Users",
                newName: "Units_PressureUnit");

            migrationBuilder.RenameColumn(
                name: "FeelLike",
                table: "Users",
                newName: "Units_FeelLike");

            migrationBuilder.RenameColumn(
                name: "Timezone",
                table: "Stations",
                newName: "Location_Timezone");

            migrationBuilder.RenameColumn(
                name: "StaticMap",
                table: "Stations",
                newName: "Location_StaticMap");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Stations",
                newName: "Location_GeoLocation_Longitude");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Stations",
                newName: "Location_GeoLocation_Latitude");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Stations",
                newName: "Location_City");

            migrationBuilder.RenameColumn(
                name: "Altitude",
                table: "Stations",
                newName: "Location_Altitude");

            migrationBuilder.RenameColumn(
                name: "BatteryVp",
                table: "Devices",
                newName: "Battery_Vp");

            migrationBuilder.RenameColumn(
                name: "BatteryPercent",
                table: "Devices",
                newName: "Battery_Percent");

            migrationBuilder.RenameColumn(
                name: "OutdoorDashboardData_TemperatureMinTimestamp",
                table: "DashboardData",
                newName: "TemperatureMinMax_Temperature_Min_Timestamp1");

            migrationBuilder.RenameColumn(
                name: "OutdoorDashboardData_TemperatureMin",
                table: "DashboardData",
                newName: "TemperatureMinMax_Temperature_Min_Value1");

            migrationBuilder.RenameColumn(
                name: "OutdoorDashboardData_TemperatureMaxTimestamp",
                table: "DashboardData",
                newName: "TemperatureMinMax_Temperature_Min_Timestamp");

            migrationBuilder.RenameColumn(
                name: "OutdoorDashboardData_TemperatureMax",
                table: "DashboardData",
                newName: "TemperatureMinMax_Temperature_Min_Value");

            migrationBuilder.RenameColumn(
                name: "OutdoorDashboardData_Temperature",
                table: "DashboardData",
                newName: "Temperature_Min_Value");

            migrationBuilder.RenameColumn(
                name: "MainDashboardData_TemperatureMinTimestamp",
                table: "DashboardData",
                newName: "Temperature_Min_Timestamp");

            migrationBuilder.RenameColumn(
                name: "MainDashboardData_TemperatureMin",
                table: "DashboardData",
                newName: "TemperatureMinMax_Temperature_Max_Value1");

            migrationBuilder.RenameColumn(
                name: "MainDashboardData_TemperatureMaxTimestamp",
                table: "DashboardData",
                newName: "TemperatureMinMax_Temperature_Max_Timestamp1");

            migrationBuilder.RenameColumn(
                name: "MainDashboardData_TemperatureMax",
                table: "DashboardData",
                newName: "TemperatureMinMax_Temperature_Max_Value");

            migrationBuilder.RenameColumn(
                name: "MainDashboardData_Temperature",
                table: "DashboardData",
                newName: "Temperature_Max_Value");

            migrationBuilder.RenameColumn(
                name: "Pressure",
                table: "DashboardData",
                newName: "TemperatureData_Temperature_Current1");

            migrationBuilder.RenameColumn(
                name: "AbsolutePressure",
                table: "DashboardData",
                newName: "TemperatureData_Temperature_Current");

            migrationBuilder.RenameColumn(
                name: "TemperatureMinTimestamp",
                table: "DashboardData",
                newName: "TemperatureMinMax_Temperature_Max_Timestamp");

            migrationBuilder.RenameColumn(
                name: "TemperatureMin",
                table: "DashboardData",
                newName: "Temperature_Current");

            migrationBuilder.RenameColumn(
                name: "TemperatureMaxTimestamp",
                table: "DashboardData",
                newName: "Temperature_Max_Timestamp");

            migrationBuilder.RenameColumn(
                name: "TemperatureMax",
                table: "DashboardData",
                newName: "Pressure_Value");

            migrationBuilder.RenameColumn(
                name: "Temperature",
                table: "DashboardData",
                newName: "Pressure_Absolute");

            migrationBuilder.AddColumn<string>(
                name: "Location_Country",
                table: "Stations",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Pressure_Trend",
                table: "DashboardData",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Temperature_Trend",
                table: "DashboardData",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TemperatureData_Temperature_Trend",
                table: "DashboardData",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TemperatureData_Temperature_Trend1",
                table: "DashboardData",
                nullable: true);
        }
    }
}
