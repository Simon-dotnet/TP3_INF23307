using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nordik_Aventure.Migrations
{
    /// <inheritdoc />
    public partial class AjouterClientEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "Email", "Name", "Password", "Phone", "SatisfactionLevel", "Status", "Type" },
                values: new object[,]
                {
                    { 1, "144 rue de paul, Lévis, Qc, Canada", "paul@paul.ca", "Paul", "Paul123", "418-878-4090", null, null, "particulier" },
                    { 2, "123 rue du kayak, Montréal, Qc, Canada", "kayak@kayak.ca", "KayakManiac", "Kayak123", "418-878-4990", null, null, "entreprise" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "EmailAddress", "HireDate", "Name", "Password", "PhoneNumber", "Role", "Surname" },
                values: new object[,]
                {
                    { 1, "marc123@gmail.com", new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Local), "Marc", "marc123*", "418-882-8636", "Employee", "Leblond" },
                    { 2, "jean123@gmail.com", new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Local), "Jean", "jean123*", "418-882-8646", "Manager", "Laronde" },
                    { 3, "money@gmail.com", new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Local), "Arjean", "money123*", "418-182-8646", "Accountant", "Labonde" }
                });
        }

        /// <inheritdoc />
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
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
