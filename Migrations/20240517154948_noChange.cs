using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeneralWorkPermit.Migrations
{
    public partial class noChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10f86fa4-9982-41bf-90d3-5c688c81aa0b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16894f4e-c031-4474-8fa7-504079163a27");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3c6830d-8d20-4774-b606-9d877d73ad48");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 256);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "42ababe3-81ce-454f-9fb7-77c321107acc", "22a4d9b1-17a4-4e27-8ed0-ba40cb560634", "Inspector", "INSPECTOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "95a20fc5-b42f-4193-bb19-6922fdf570b3", "957774aa-2a51-496e-b2b1-6b4e1e4a74ab", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e3ca1a60-c7f4-4987-bcc4-d5789beed23a", "71cbe472-f35d-48cb-9d5e-1510ef2d1356", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42ababe3-81ce-454f-9fb7-77c321107acc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95a20fc5-b42f-4193-bb19-6922fdf570b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3ca1a60-c7f4-4987-bcc4-d5789beed23a");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "10f86fa4-9982-41bf-90d3-5c688c81aa0b", "aa9cbbd6-e829-4661-a8be-01b29dad43b1", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "16894f4e-c031-4474-8fa7-504079163a27", "5b74e208-1776-4e8b-8abb-9c0614d7fd68", "Inspector", "INSPECTOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a3c6830d-8d20-4774-b606-9d877d73ad48", "80a52ea0-97e7-4d5d-87d2-112fd88d3190", "Admin", "ADMIN" });
        }
    }
}
