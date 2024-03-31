using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TokenAndRefresh.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserAndRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Key = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "HistorialRefreshToken",
                columns: table => new
                {
                    IdHistoryRefreshToken = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(type: "int", nullable: true),
                    Token = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    RefreshToken = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateExpiration = table.Column<DateTime>(type: "datetime", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true, computedColumnSql: "(case when [DateExpiration]<getdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", stored: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__History", x => x.IdHistoryRefreshToken);
                    table.ForeignKey(
                        name: "FK__History__IdUser",
                        column: x => x.IdUser,
                        principalTable: "Usuario",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistorialRefreshToken_IdUser",
                table: "HistorialRefreshToken",
                column: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorialRefreshToken");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
