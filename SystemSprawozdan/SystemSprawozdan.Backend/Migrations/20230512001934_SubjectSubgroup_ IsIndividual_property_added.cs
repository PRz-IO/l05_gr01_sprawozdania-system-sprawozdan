using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemSprawozdan.Backend.Migrations
{
    /// <inheritdoc />
    public partial class SubjectSubgroup_IsIndividual_property_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsIndividual",
                table: "SubjectSubgroup",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIndividual",
                table: "SubjectSubgroup");
        }
    }
}
