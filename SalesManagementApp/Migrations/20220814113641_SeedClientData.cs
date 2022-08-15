using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManagementApp.Migrations
{
    public partial class SeedClientData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Email", "FirstName", "JobTitle", "LastName", "PhoneNumber", "RetailOutletId" },
                values: new object[,]
                {
                    { 1, "james.tailor@company.com", "James", "Buyer", "Tailor", "000000000", 1 },
                    { 2, "jill.hutton@company.com", "Jill", "Buyer", "Hutton", "000000000", 2 },
                    { 3, "craig.rice@company.com", "Craig", "Buyer", "Rice", "000000000", 3 },
                    { 4, "amy.smith@company.com", "Amy", "Buyer", "Smith", "000000000", 4 }
                });

            migrationBuilder.InsertData(
                table: "RetailOutlet",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { 1, "TX", "Texas Outdoor Store" },
                    { 2, "CA", "California Outdoor Store" },
                    { 3, "NY", "New York Outdoor Store" },
                    { 4, "WA", " Washington Outdoor Store" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RetailOutlet",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RetailOutlet",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RetailOutlet",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RetailOutlet",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
