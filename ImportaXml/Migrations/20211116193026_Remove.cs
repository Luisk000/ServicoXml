using Microsoft.EntityFrameworkCore.Migrations;

namespace ImportaXml.Migrations
{
    public partial class Remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Internos_Files_XmlFileId",
                table: "Internos");

            migrationBuilder.DropIndex(
                name: "IX_Internos_XmlFileId",
                table: "Internos");

            migrationBuilder.DropColumn(
                name: "XmlFileId",
                table: "Internos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "XmlFileId",
                table: "Internos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Internos_XmlFileId",
                table: "Internos",
                column: "XmlFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Internos_Files_XmlFileId",
                table: "Internos",
                column: "XmlFileId",
                principalTable: "Files",
                principalColumn: "nfeProc_NFe_infNFe_____Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
