using Microsoft.EntityFrameworkCore.Migrations;

namespace ImportaXml.Migrations
{
    public partial class Transfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Transferido",
                table: "Files");

            migrationBuilder.AddColumn<bool>(
                name: "Transferido",
                table: "Dets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Transferido",
                table: "Dets");

            migrationBuilder.AddColumn<bool>(
                name: "Transferido",
                table: "Files",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
