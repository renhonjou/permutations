using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace permutationsWeb.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermutationsResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PermutationsJson = table.Column<string>(nullable: false),
                    Request = table.Column<string>(maxLength: 8, nullable: false),
                    Seconds = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermutationsResults", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermutationsResults_Request",
                table: "PermutationsResults",
                column: "Request",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermutationsResults");
        }
    }
}
