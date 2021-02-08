using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScuffedAuth.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorizationCodes",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresIn = table.Column<int>(type: "int", nullable: false),
                    RedirectUri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizationCodes", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(74)", maxLength: 74, nullable: false),
                    RedirectUri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Value = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TokenType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresIn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Value);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "RedirectUri", "Secret" },
                values: new object[] { "clientId", null, "1000.39zyePe+fstN7VVEitrNyg==.fDCT8OLtWjHKhotdLb43EJm0jBehkp6J45NGyMvFYAw=" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "RedirectUri", "Secret" },
                values: new object[] { "c90c4832101ee1cf19c859e276527867", null, "1000.15qVsWgYLYz1X5qaNaF+Fg==.3jQRY0tAwXEJ7ulvfEx6+FJd0Q0w35b9BZNea9tmlI4=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorizationCodes");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Tokens");
        }
    }
}
