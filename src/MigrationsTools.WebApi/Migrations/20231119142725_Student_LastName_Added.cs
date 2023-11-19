using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MigrationsTools.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Student_LastName_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Students");
        }
    }
}
