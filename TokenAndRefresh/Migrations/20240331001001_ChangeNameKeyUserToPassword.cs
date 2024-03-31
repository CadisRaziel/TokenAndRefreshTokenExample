using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TokenAndRefresh.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameKeyUserToPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KeyUser",
                table: "Usuario",
                newName: "Password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Usuario",
                newName: "KeyUser");
        }
    }
}
