using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIChallenge.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_PASSWORD = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    USER_EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.USER_ID);
                });

            migrationBuilder.CreateTable(
                name: "SECURITY",
                columns: table => new
                {
                    USER_ID = table.Column<int>(type: "int", nullable: false),
                    SALT_PASSWORD = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SECURITY", x => x.USER_ID);
                    table.ForeignKey(
                        name: "FK_SECURITY_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SECURITY");

            migrationBuilder.DropTable(
                name: "USER");
        }
    }
}
