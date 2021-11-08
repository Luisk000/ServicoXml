using Microsoft.EntityFrameworkCore.Migrations;

namespace ImportaXml.Migrations
{
    public partial class n : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_pPIS",
                table: "Dets");

            migrationBuilder.DropColumn(
                name: "nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_vPIS",
                table: "Dets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_pPIS",
                table: "Dets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_vPIS",
                table: "Dets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
