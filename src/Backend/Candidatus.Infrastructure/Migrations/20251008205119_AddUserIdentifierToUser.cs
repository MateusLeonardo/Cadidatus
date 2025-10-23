using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidatus.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddUserIdentifierToUser : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "UserIdentifier",
            table: "users",
            type: "char(36)",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
            collation: "ascii_general_ci");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "UserIdentifier",
            table: "users");
    }
}
