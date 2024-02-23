using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    /// <inheritdoc />
    public partial class TotalAnnualSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TesteTokenValid");

            migrationBuilder.AddColumn<double>(
                name: "BaseSalary",
                table: "Analytics",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Commission",
                table: "Analytics",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PaidWeeklyRest",
                table: "Analytics",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TotalAnnualSalesId",
                table: "Analytics",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalPayment",
                table: "Analytics",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TotalAnnualSales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    January = table.Column<double>(type: "float", nullable: true),
                    February = table.Column<double>(type: "float", nullable: true),
                    March = table.Column<double>(type: "float", nullable: true),
                    April = table.Column<double>(type: "float", nullable: true),
                    May = table.Column<double>(type: "float", nullable: true),
                    June = table.Column<double>(type: "float", nullable: true),
                    July = table.Column<double>(type: "float", nullable: true),
                    August = table.Column<double>(type: "float", nullable: true),
                    September = table.Column<double>(type: "float", nullable: true),
                    Octuber = table.Column<double>(type: "float", nullable: true),
                    November = table.Column<double>(type: "float", nullable: true),
                    December = table.Column<double>(type: "float", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalAnnualSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TotalAnnualSales_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analytics_TotalAnnualSalesId",
                table: "Analytics",
                column: "TotalAnnualSalesId");

            migrationBuilder.CreateIndex(
                name: "IX_TotalAnnualSales_UserId",
                table: "TotalAnnualSales",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Analytics_TotalAnnualSales_TotalAnnualSalesId",
                table: "Analytics",
                column: "TotalAnnualSalesId",
                principalTable: "TotalAnnualSales",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analytics_TotalAnnualSales_TotalAnnualSalesId",
                table: "Analytics");

            migrationBuilder.DropTable(
                name: "TotalAnnualSales");

            migrationBuilder.DropIndex(
                name: "IX_Analytics_TotalAnnualSalesId",
                table: "Analytics");

            migrationBuilder.DropColumn(
                name: "BaseSalary",
                table: "Analytics");

            migrationBuilder.DropColumn(
                name: "Commission",
                table: "Analytics");

            migrationBuilder.DropColumn(
                name: "PaidWeeklyRest",
                table: "Analytics");

            migrationBuilder.DropColumn(
                name: "TotalAnnualSalesId",
                table: "Analytics");

            migrationBuilder.DropColumn(
                name: "TotalPayment",
                table: "Analytics");

            migrationBuilder.CreateTable(
                name: "TesteTokenValid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TesteTokenValid", x => x.Id);
                });
        }
    }
}
