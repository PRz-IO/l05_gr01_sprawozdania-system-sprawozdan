using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SystemSprawozdan.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Added_term_again : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TermObjectId",
                table: "Subject",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Term",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TermNumber = table.Column<int>(type: "integer", nullable: false),
                    StartedAt = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Term", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subject_TermObjectId",
                table: "Subject",
                column: "TermObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Term_TermObjectId",
                table: "Subject",
                column: "TermObjectId",
                principalTable: "Term",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Term_TermObjectId",
                table: "Subject");

            migrationBuilder.DropTable(
                name: "Term");

            migrationBuilder.DropIndex(
                name: "IX_Subject_TermObjectId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "TermObjectId",
                table: "Subject");
        }
    }
}
