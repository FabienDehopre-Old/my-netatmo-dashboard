using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Netatmo.Dashboard.Data.Migrations
{
    public partial class AddSeedDataForUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessToken", "Enabled", "ExpiresAt", "FeelLike", "PressureUnit", "RefreshToken", "Uid", "Unit", "UpdateJobId", "WindUnit" },
                values: new object[] { 1, "56102b6fc6aa42f174e5d484|a2a52b7b24acfacf1718f69bbf226620", true, new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "56102b6fc6aa42f174e5d484|73d5b91c1cb4b021fddf91634be0a598", "auth0|5c3369d9b171c101904570ca", null, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
