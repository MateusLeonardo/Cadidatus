using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidatus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCompaniesTableAddCityId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_companies_CityId",
                table: "companies",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_companies_cities_CityId",
                table: "companies",
                column: "CityId",
                principalTable: "cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_companies_cities_CityId",
                table: "companies");

            migrationBuilder.DropIndex(
                name: "IX_companies_CityId",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "companies");
        }
    }
}
