using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeneralWorkPermit.Migrations
{
    public partial class addApplicantForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ApplicantId",
                table: "reviews",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6d1f655c-b0db-443c-bb6f-fdf8df1ca955", "f1017cfa-7e8c-4df5-acaa-aac90a8165f5", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "77834870-6ea4-4678-9dfa-156896bc614d", "d185fc37-83d7-49c1-be2c-b27d629208e0", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b06fd7d9-1729-4d45-af72-6481ef5f7199", "95b9ebb8-5f08-42cc-852d-0066b37b6fd3", "Inspector", "INSPECTOR" });

            migrationBuilder.CreateIndex(
                name: "IX_reviews_ApplicantId",
                table: "reviews",
                column: "ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_AspNetUsers_ApplicantId",
                table: "reviews",
                column: "ApplicantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_AspNetUsers_ApplicantId",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_reviews_ApplicantId",
                table: "reviews");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6d1f655c-b0db-443c-bb6f-fdf8df1ca955");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77834870-6ea4-4678-9dfa-156896bc614d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b06fd7d9-1729-4d45-af72-6481ef5f7199");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "reviews");

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
    }
}
