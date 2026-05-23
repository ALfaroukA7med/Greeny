using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greeny.DAL.Migrations
{
    /// <inheritdoc />
    public partial class imagefile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Posts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Posts",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
