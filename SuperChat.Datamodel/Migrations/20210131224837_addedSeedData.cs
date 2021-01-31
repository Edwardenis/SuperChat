using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperChat.Datamodel.Migrations
{
    public partial class addedSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ChatRooms",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "Deleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, "Test1", new DateTimeOffset(new DateTime(2021, 1, 31, 22, 48, 37, 548, DateTimeKind.Unspecified).AddTicks(6155), new TimeSpan(0, 0, 0, 0, 0)), null, false, "Default Chat room", null, null });

            migrationBuilder.InsertData(
                table: "ChatRooms",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "Deleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 2, "Test2", new DateTimeOffset(new DateTime(2021, 1, 31, 22, 48, 37, 548, DateTimeKind.Unspecified).AddTicks(6907), new TimeSpan(0, 0, 0, 0, 0)), null, false, "Default Chat room 2", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChatRooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ChatRooms",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
