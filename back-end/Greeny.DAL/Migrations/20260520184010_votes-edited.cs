using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greeny.DAL.Migrations
{
    /// <inheritdoc />
    public partial class votesedited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUpvote",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Votes");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Votes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Votes");

            migrationBuilder.AddColumn<bool>(
                name: "IsUpvote",
                table: "Votes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Votes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
