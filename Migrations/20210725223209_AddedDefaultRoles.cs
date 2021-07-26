using Microsoft.EntityFrameworkCore.Migrations;

namespace MyHotelListing.Migrations
{
    public partial class AddedDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "01b4fc43-4a3f-4d71-b0df-1741f37cb05b", "a3d0b3a4-40f4-4dbe-9d47-8243f8162f52", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "456d9779-e2ca-4813-bb93-7218583e6f83", "02c75920-64f9-4f5e-a986-a193d6f83702", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01b4fc43-4a3f-4d71-b0df-1741f37cb05b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "456d9779-e2ca-4813-bb93-7218583e6f83");
        }
    }
}
