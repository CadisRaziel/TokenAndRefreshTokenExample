using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TokenAndRefresh.Migrations
{
    /// <inheritdoc />
    public partial class ChangeKeyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Usuario",
                newName: "KeyUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KeyUser",
                table: "Usuario",
                newName: "Key");
        }
    }
}
