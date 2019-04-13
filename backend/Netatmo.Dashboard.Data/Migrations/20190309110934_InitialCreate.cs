using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Netatmo.Dashboard.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Uid = table.Column<string>(maxLength: 32, nullable: false),
                    Enabled = table.Column<bool>(nullable: false, defaultValue: false),
                    AccessToken = table.Column<string>(maxLength: 64, nullable: true),
                    ExpiresAt = table.Column<DateTime>(nullable: true),
                    RefreshToken = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Location_Altitude = table.Column<decimal>(nullable: false),
                    Location_City = table.Column<string>(maxLength: 256, nullable: false),
                    Location_Country = table.Column<string>(maxLength: 256, nullable: false),
                    Location_GeoLocation_Latitude = table.Column<decimal>(nullable: false),
                    Location_GeoLocation_Longitude = table.Column<decimal>(nullable: false),
                    Location_Timezone = table.Column<string>(maxLength: 32, nullable: false),
                    Location_StaticMap = table.Column<string>(maxLength: 1024, nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 17, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Firmware = table.Column<int>(nullable: false),
                    StationId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    WifiStatus = table.Column<int>(nullable: true),
                    RfStatus = table.Column<int>(nullable: true),
                    Battery_Vp = table.Column<int>(nullable: true),
                    Battery_Percent = table.Column<decimal>(nullable: true),
                    Type = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DashboardData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeUtc = table.Column<DateTime>(nullable: false),
                    DeviceId = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Temperature_Current = table.Column<decimal>(nullable: true),
                    Temperature_Min_Value = table.Column<decimal>(nullable: true),
                    Temperature_Min_Timestamp = table.Column<DateTime>(nullable: true),
                    Temperature_Max_Value = table.Column<decimal>(nullable: true),
                    Temperature_Max_Timestamp = table.Column<DateTime>(nullable: true),
                    Temperature_Trend = table.Column<int>(nullable: true),
                    CO2 = table.Column<int>(nullable: true),
                    Humidity = table.Column<int>(nullable: true),
                    TemperatureData_Temperature_Current = table.Column<decimal>(nullable: true),
                    TemperatureMinMax_Temperature_Min_Value = table.Column<decimal>(nullable: true),
                    TemperatureMinMax_Temperature_Min_Timestamp = table.Column<DateTime>(nullable: true),
                    TemperatureMinMax_Temperature_Max_Value = table.Column<decimal>(nullable: true),
                    TemperatureMinMax_Temperature_Max_Timestamp = table.Column<DateTime>(nullable: true),
                    TemperatureData_Temperature_Trend = table.Column<int>(nullable: true),
                    Pressure_Value = table.Column<decimal>(nullable: true),
                    Pressure_Absolute = table.Column<decimal>(nullable: true),
                    Pressure_Trend = table.Column<int>(nullable: true),
                    MainDashboardData_CO2 = table.Column<int>(nullable: true),
                    MainDashboardData_Humidity = table.Column<int>(nullable: true),
                    Noise = table.Column<int>(nullable: true),
                    TemperatureData_Temperature_Current1 = table.Column<decimal>(nullable: true),
                    TemperatureMinMax_Temperature_Min_Value1 = table.Column<decimal>(nullable: true),
                    TemperatureMinMax_Temperature_Min_Timestamp1 = table.Column<DateTime>(nullable: true),
                    TemperatureMinMax_Temperature_Max_Value1 = table.Column<decimal>(nullable: true),
                    TemperatureMinMax_Temperature_Max_Timestamp1 = table.Column<DateTime>(nullable: true),
                    TemperatureData_Temperature_Trend1 = table.Column<int>(nullable: true),
                    OutdoorDashboardData_Humidity = table.Column<int>(nullable: true),
                    Rain = table.Column<decimal>(nullable: true),
                    Sum1H = table.Column<decimal>(nullable: true),
                    Sum24H = table.Column<decimal>(nullable: true),
                    WindStrength = table.Column<int>(nullable: true),
                    WindAngle = table.Column<int>(nullable: true),
                    GustStrength = table.Column<int>(nullable: true),
                    GustAngle = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DashboardData_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DashboardData_DeviceId",
                table: "DashboardData",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_StationId",
                table: "Devices",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_UserId",
                table: "Stations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Uid",
                table: "Users",
                column: "Uid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DashboardData");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
