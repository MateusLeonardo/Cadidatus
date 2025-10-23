using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidatus.Infrastructure.Migrations;

/// <inheritdoc />
public partial class UpdateStatesTableAddColumnUserId : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_states_Uf",
            table: "states");

        migrationBuilder.AddColumn<int>(
            name: "UserId",
            table: "states",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateIndex(
            name: "IX_states_UserId",
            table: "states",
            column: "UserId");

        migrationBuilder.AddForeignKey(
            name: "FK_states_users_UserId",
            table: "states",
            column: "UserId",
            principalTable: "users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_states_users_UserId",
            table: "states");

        migrationBuilder.DropIndex(
            name: "IX_states_UserId",
            table: "states");

        migrationBuilder.DropColumn(
            name: "UserId",
            table: "states");

        migrationBuilder.CreateIndex(
            name: "IX_states_Uf",
            table: "states",
            column: "Uf",
            unique: true);
    }
}
