using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemSprawozdan.Backend.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Terms",
                table: "Terms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Majors",
                table: "Majors");

            migrationBuilder.RenameTable(
                name: "Terms",
                newName: "Term");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Student");

            migrationBuilder.RenameTable(
                name: "Majors",
                newName: "Major");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Student",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Term",
                table: "Term",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Major",
                table: "Major",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Term",
                table: "Term");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Major",
                table: "Major");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Student");

            migrationBuilder.RenameTable(
                name: "Term",
                newName: "Terms");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "Major",
                newName: "Majors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Terms",
                table: "Terms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Majors",
                table: "Majors",
                column: "Id");
        }
    }
}
