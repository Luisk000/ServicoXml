using Microsoft.EntityFrameworkCore.Migrations;

namespace ImportaXml.Migrations
{
    public partial class NM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    nfeProc_NFe_infNFe_____Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    XmlName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc____versao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc____xmlns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_Nfe____xmlns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_Nfe_infNFe____versao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_ide_cUF = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_ide_cNF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_ide_natOp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_ide_mod = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_ide_serie = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_ide_nNF = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_ide_dhEmi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_ide_dhSaiEnt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_ide_tpNF = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_ide_idDest = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_ide_cMunFG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_ide_tpImp = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_ide_tpEmis = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_ide_cDV = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_ide_tpAmb = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_ide_finNFe = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_ide_indFinal = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_ide_indPres = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_ide_indIntermed = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_ide_procEmi = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_ide_verProc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_CPF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_xNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_xFant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_enderEmit_xLgr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_enderEmit_nro = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_emit_enderEmit_xCpl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_enderEmit_xBairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_enderEmit_cMun = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_enderEmit_xMun = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_enderEmit_UF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_enderEmit_CEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_enderEmit_cPais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_enderEmit_xPais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_enderEmit_fone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_IE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_IEST = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_IM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_CNAE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_CRT = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_emit_indIEDest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_emit_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_dest_CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_dest_CPF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_dest_idEstrangeiro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_dest_xNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_dest_enderDest_xLgr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_dest_enderDest_nro = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_dest_enderDest_xCpl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_dest_enderDest_xBairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_dest_enderDest_cMun = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_dest_enderDest_xMun = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_dest_enderDest_UF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_dest_enderDest_CEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_dest_enderDest_cPais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_dest_enderDest_xPais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_dest_enderDest_fone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_dest_indIEDest = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_dest_IE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_dest_ISUF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_dest_IM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_dest_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_retirada_CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_retirada_xNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_retirada_xLgr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_retirada_nro = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_retirada_xBairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_retirada_cMun = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_retirada_xMun = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_retirada_UF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_retirada_CEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_retirada_cPais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_retirada_xPais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_retirada_fone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_retirada_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_retirada_IE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_entrega_CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_entrega_xNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_entrega_xLgr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_entrega_nro = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_entrega_xBairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_entrega_cMun = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_entrega_xMun = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_entrega_UF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_entrega_CEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_entrega_cPais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_entrega_xPais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_entrega_fone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_entrega_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_entrega_IE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_autXml_CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_autXml_CPF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_total_ICMSTot_vBC = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vICMS = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vICMSDeson = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vFCPUFDest = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vICMSUFDest = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vICMSUFRemet = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vFCP = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vBCST = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vST = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vFCPST = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vFCPSTRet = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vProd = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vFrete = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vSeg = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vDesc = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vII = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vIPI = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vIPIDevol = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vPIS = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vCOFINS = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vOutro = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vNF = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_ICMSTot_vTotTrib = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_transp_modFrete = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_total_transp_transporta_CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_total_transp_transporta_xNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_total_transp_transporta_IE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_total_transp_transporta_xEnder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_total_transp_transporta_xMun = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_total_transp_transporta_UF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_total_transp_veicTransp_placa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_total_transp_veicTransp_UF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_total_transp_vol_esp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_total_transp_vol_qVol = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_total_transp_vol_marca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_total_transp_vol_pesoL = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_transp_vol_pesoB = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_cobr_fat_nFat = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_cobr_fat_vOrig = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_cobr_fat_vDesc = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_cobr_fat_vLiq = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_cobr_dup_nDup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_total_cobr_dup_dVenc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_total_cobr_dup_vDup = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_total_pag_detPag_indPag = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_total_pag_detPag_tPag = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_total_pag_detPag_vPag = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_infAdic_infCpl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_infRespTec_CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_infRespTec_xContato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_infRespTec_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_infRespTec_fone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_Signature____xmlns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_Signature_SignedInfo____xmlns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_Signature_SignedInfo_CanonicalizationMethod____Algorithm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_Signature_SignedInfo_SignatureMethod____Algorithm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_Signature_SignedInfo_Reference____URI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_Signature_SignedInfo_Reference_DigestMethod____Algorithm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_Signature_SignedInfo_Reference_DigestValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_Signature_SignatureValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_Signature_SignatureValue____xmlns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_Signature_KeyInfo____xmlns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_Signature_KeyInfo_X509Data_X509Certificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_protNFe____versao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_protNFe____xmlns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_protNFe_infProt____xmlns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_protNFe_infProt____Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_protNFe_infProt_tpAmb = table.Column<int>(type: "int", nullable: false),
                    nfeProc_protNFe_infProt_verAplic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_protNFe_infProt_chNFe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_protNFe_infProt_dhRecbto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_protNFe_infProt_nProt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_protNFe_infProt_digVal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_protNFe_infProt_cStat = table.Column<int>(type: "int", nullable: false),
                    nfeProc_protNFe_infProt_xMotivo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.nfeProc_NFe_infNFe_____Id);
                });

            migrationBuilder.CreateTable(
                name: "Algorithms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    XmlFileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    nfeProc_NFe_Signature_SignedInfo_Reference_Transforms_Transform____Algorithm = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Algorithms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Algorithms_Files_XmlFileId",
                        column: x => x.XmlFileId,
                        principalTable: "Files",
                        principalColumn: "nfeProc_NFe_infNFe_____Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    XmlFileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    nfeProc_NFe_infNFe_det____nItem = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_det_prod_cProd = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_det_prod_cEAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_det_prod_xProd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_det_prod_NCM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_det_prod_CFOP = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_det_prod_uCom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_det_prod_qCom = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_det_prod_vUnCom = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_det_prod_vProd = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_det_prod_cEANTrib = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_det_prod_uTrib = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nfeProc_NFe_infNFe_det_prod_qTrib = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_det_prod_vUnTrib = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_det_prod_indTot = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMSN102_orig = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMSN102_CSOSN = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_orig = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_CST = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_modBC = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_vBC = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_pICMS = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_vICMS = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_IPI_cEnq = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_IPI_IPINT_CST = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_IPI_IPINTrib_CST = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_IPI_IPINTrib_vBC = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_IPI_IPINTrib_pIPI = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_IPI_IPINTrib_vIPI = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_PIS_PISNT_CST = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_PIS_PISAliq_CST = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_PIS_PISAliq_vBC = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_PIS_PISAliq_pPIS = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_PIS_PISAliq_vPIS = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSNT_CST = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_CST = table.Column<int>(type: "int", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_vBC = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_pCOFINS = table.Column<double>(type: "float", nullable: false),
                    nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_vCOFINS = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dets_Files_XmlFileId",
                        column: x => x.XmlFileId,
                        principalTable: "Files",
                        principalColumn: "nfeProc_NFe_infNFe_____Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NRs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    XmlFileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InfoNaoRegistrada = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NRs_Files_XmlFileId",
                        column: x => x.XmlFileId,
                        principalTable: "Files",
                        principalColumn: "nfeProc_NFe_infNFe_____Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Algorithms_XmlFileId",
                table: "Algorithms",
                column: "XmlFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Dets_XmlFileId",
                table: "Dets",
                column: "XmlFileId");

            migrationBuilder.CreateIndex(
                name: "IX_NRs_XmlFileId",
                table: "NRs",
                column: "XmlFileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Algorithms");

            migrationBuilder.DropTable(
                name: "Dets");

            migrationBuilder.DropTable(
                name: "NRs");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
