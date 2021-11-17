﻿// <auto-generated />
using ImportaXml.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ImportaXml.Migrations
{
    [DbContext(typeof(XmlDbContext))]
    partial class XmlDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ImportaXml.Models.Interno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("CodEmpresaLocal")
                        .HasColumnType("float");

                    b.Property<double>("CodFornecedorLocal")
                        .HasColumnType("float");

                    b.Property<double>("CodProdLocal")
                        .HasColumnType("float");

                    b.Property<string>("Empresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fornecedor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Produto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Internos");
                });

            modelBuilder.Entity("ImportaXml.Models.XmlFile", b =>
                {
                    b.Property<string>("nfeProc_NFe_infNFe_____Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("XmlName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_Signature_KeyInfo_X509Data_X509Certificate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_Signature_KeyInfo____xmlns")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_Signature_SignatureValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_Signature_SignatureValue____xmlns")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_Signature_SignedInfo_CanonicalizationMethod____Algorithm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_Signature_SignedInfo_Reference_DigestMethod____Algorithm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_Signature_SignedInfo_Reference_DigestValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_Signature_SignedInfo_Reference____URI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_Signature_SignedInfo_SignatureMethod____Algorithm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_Signature_SignedInfo____xmlns")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_Signature____xmlns")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_autXml_CNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_autXml_CPF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_dest_CNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_dest_CPF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_dest_IE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_dest_IM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_dest_ISUF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_dest_email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_dest_enderDest_CEP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_dest_enderDest_UF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_dest_enderDest_cMun")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_dest_enderDest_cPais")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_dest_enderDest_fone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nfeProc_NFe_infNFe_dest_enderDest_nro")
                        .HasColumnType("int");

                    b.Property<string>("nfeProc_NFe_infNFe_dest_enderDest_xBairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_dest_enderDest_xCpl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_dest_enderDest_xLgr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_dest_enderDest_xMun")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_dest_enderDest_xPais")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_dest_idEstrangeiro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nfeProc_NFe_infNFe_dest_indIEDest")
                        .HasColumnType("int");

                    b.Property<string>("nfeProc_NFe_infNFe_dest_xNome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_CNAE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_CNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_CPF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nfeProc_NFe_infNFe_emit_CRT")
                        .HasColumnType("int");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_IE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_IEST")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_IM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_enderEmit_CEP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_enderEmit_UF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_enderEmit_cMun")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_enderEmit_cPais")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_enderEmit_fone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nfeProc_NFe_infNFe_emit_enderEmit_nro")
                        .HasColumnType("int");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_enderEmit_xBairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_enderEmit_xCpl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_enderEmit_xLgr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_enderEmit_xMun")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_enderEmit_xPais")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_indIEDest")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_xFant")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_emit_xNome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_entrega_CEP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_entrega_CNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_entrega_IE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_entrega_UF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_entrega_cMun")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_entrega_cPais")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_entrega_email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_entrega_fone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nfeProc_NFe_infNFe_entrega_nro")
                        .HasColumnType("int");

                    b.Property<string>("nfeProc_NFe_infNFe_entrega_xBairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_entrega_xLgr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_entrega_xMun")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_entrega_xNome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_entrega_xPais")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nfeProc_NFe_infNFe_ide_cDV")
                        .HasColumnType("int");

                    b.Property<string>("nfeProc_NFe_infNFe_ide_cMunFG")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_ide_cNF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nfeProc_NFe_infNFe_ide_cUF")
                        .HasColumnType("int");

                    b.Property<string>("nfeProc_NFe_infNFe_ide_dhEmi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_ide_dhSaiEnt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nfeProc_NFe_infNFe_ide_finNFe")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_ide_idDest")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_ide_indFinal")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_ide_indIntermed")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_ide_indPres")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_ide_mod")
                        .HasColumnType("int");

                    b.Property<double>("nfeProc_NFe_infNFe_ide_nNF")
                        .HasColumnType("float");

                    b.Property<string>("nfeProc_NFe_infNFe_ide_natOp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nfeProc_NFe_infNFe_ide_procEmi")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_ide_serie")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_ide_tpAmb")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_ide_tpEmis")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_ide_tpImp")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_ide_tpNF")
                        .HasColumnType("int");

                    b.Property<string>("nfeProc_NFe_infNFe_ide_verProc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_infAdic_infCpl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_infRespTec_CNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_infRespTec_email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_infRespTec_fone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_infRespTec_xContato")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_retirada_CEP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_retirada_CNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_retirada_IE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_retirada_UF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_retirada_cMun")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_retirada_cPais")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_retirada_email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_retirada_fone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nfeProc_NFe_infNFe_retirada_nro")
                        .HasColumnType("int");

                    b.Property<string>("nfeProc_NFe_infNFe_retirada_xBairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_retirada_xLgr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_retirada_xMun")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_retirada_xNome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_retirada_xPais")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vBC")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vBCST")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vCOFINS")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vDesc")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vFCP")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vFCPST")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vFCPSTRet")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vFCPUFDest")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vFrete")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vICMS")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vICMSDeson")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vICMSUFDest")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vICMSUFRemet")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vII")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vIPI")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vIPIDevol")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vNF")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vOutro")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vPIS")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vProd")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vST")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vSeg")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_ICMSTot_vTotTrib")
                        .HasColumnType("float");

                    b.Property<string>("nfeProc_NFe_infNFe_total_cobr_dup_dVenc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_total_cobr_dup_nDup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("nfeProc_NFe_infNFe_total_cobr_dup_vDup")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_cobr_fat_nFat")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_cobr_fat_vDesc")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_cobr_fat_vLiq")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_cobr_fat_vOrig")
                        .HasColumnType("float");

                    b.Property<int>("nfeProc_NFe_infNFe_total_pag_detPag_indPag")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_total_pag_detPag_tPag")
                        .HasColumnType("int");

                    b.Property<double>("nfeProc_NFe_infNFe_total_pag_detPag_vPag")
                        .HasColumnType("float");

                    b.Property<int>("nfeProc_NFe_infNFe_total_transp_modFrete")
                        .HasColumnType("int");

                    b.Property<string>("nfeProc_NFe_infNFe_total_transp_transporta_CNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_total_transp_transporta_IE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_total_transp_transporta_UF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_total_transp_transporta_xEnder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_total_transp_transporta_xMun")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_total_transp_transporta_xNome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_total_transp_veicTransp_UF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_total_transp_veicTransp_placa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_total_transp_vol_esp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_total_transp_vol_marca")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("nfeProc_NFe_infNFe_total_transp_vol_pesoB")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_total_transp_vol_pesoL")
                        .HasColumnType("float");

                    b.Property<int>("nfeProc_NFe_infNFe_total_transp_vol_qVol")
                        .HasColumnType("int");

                    b.Property<string>("nfeProc_Nfe____xmlns")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_Nfe_infNFe____versao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc____versao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc____xmlns")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_protNFe____versao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_protNFe____xmlns")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_protNFe_infProt____Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_protNFe_infProt____xmlns")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nfeProc_protNFe_infProt_cStat")
                        .HasColumnType("int");

                    b.Property<string>("nfeProc_protNFe_infProt_chNFe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_protNFe_infProt_dhRecbto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_protNFe_infProt_digVal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_protNFe_infProt_nProt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nfeProc_protNFe_infProt_tpAmb")
                        .HasColumnType("int");

                    b.Property<string>("nfeProc_protNFe_infProt_verAplic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_protNFe_infProt_xMotivo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("nfeProc_NFe_infNFe_____Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("ImportaXml.Models.XmlFileAlgorithm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("XmlFileId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("nfeProc_NFe_Signature_SignedInfo_Reference_Transforms_Transform____Algorithm")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("XmlFileId");

                    b.ToTable("Algorithms");
                });

            modelBuilder.Entity("ImportaXml.Models.XmlFileDet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Transferido")
                        .HasColumnType("bit");

                    b.Property<string>("XmlFileId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_CST")
                        .HasColumnType("int");

                    b.Property<double>("nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_pCOFINS")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_vBC")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_vCOFINS")
                        .HasColumnType("float");

                    b.Property<int>("nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSNT_CST")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_CST")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_modBC")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_orig")
                        .HasColumnType("int");

                    b.Property<double>("nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_pICMS")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_vBC")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_vICMS")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMSN102_CSOSN")
                        .HasColumnType("float");

                    b.Property<int>("nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMSN102_orig")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_det_Imposto_IPI_IPINT_CST")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_det_Imposto_IPI_IPINTrib_CST")
                        .HasColumnType("int");

                    b.Property<double>("nfeProc_NFe_infNFe_det_Imposto_IPI_IPINTrib_pIPI")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_det_Imposto_IPI_IPINTrib_vBC")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_det_Imposto_IPI_IPINTrib_vIPI")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_det_Imposto_IPI_cEnq")
                        .HasColumnType("float");

                    b.Property<int>("nfeProc_NFe_infNFe_det_Imposto_PIS_PISAliq_CST")
                        .HasColumnType("int");

                    b.Property<double>("nfeProc_NFe_infNFe_det_Imposto_PIS_PISAliq_pPIS")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_det_Imposto_PIS_PISAliq_vBC")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_det_Imposto_PIS_PISAliq_vPIS")
                        .HasColumnType("float");

                    b.Property<int>("nfeProc_NFe_infNFe_det_Imposto_PIS_PISNT_CST")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_det____nItem")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_det_prod_CFOP")
                        .HasColumnType("int");

                    b.Property<string>("nfeProc_NFe_infNFe_det_prod_NCM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_det_prod_cEAN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_det_prod_cEANTrib")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nfeProc_NFe_infNFe_det_prod_cProd")
                        .HasColumnType("int");

                    b.Property<int>("nfeProc_NFe_infNFe_det_prod_indTot")
                        .HasColumnType("int");

                    b.Property<double>("nfeProc_NFe_infNFe_det_prod_qCom")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_det_prod_qTrib")
                        .HasColumnType("float");

                    b.Property<string>("nfeProc_NFe_infNFe_det_prod_uCom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nfeProc_NFe_infNFe_det_prod_uTrib")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("nfeProc_NFe_infNFe_det_prod_vProd")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_det_prod_vUnCom")
                        .HasColumnType("float");

                    b.Property<double>("nfeProc_NFe_infNFe_det_prod_vUnTrib")
                        .HasColumnType("float");

                    b.Property<string>("nfeProc_NFe_infNFe_det_prod_xProd")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("XmlFileId");

                    b.ToTable("Dets");
                });

            modelBuilder.Entity("ImportaXml.Models.XmlFileNaoRegistrado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("InfoNaoRegistrada")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("XmlFileId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("XmlFileId");

                    b.ToTable("NRs");
                });

            modelBuilder.Entity("ImportaXml.Models.XmlFileAlgorithm", b =>
                {
                    b.HasOne("ImportaXml.Models.XmlFile", "XmlFile")
                        .WithMany()
                        .HasForeignKey("XmlFileId");

                    b.Navigation("XmlFile");
                });

            modelBuilder.Entity("ImportaXml.Models.XmlFileDet", b =>
                {
                    b.HasOne("ImportaXml.Models.XmlFile", "XmlFile")
                        .WithMany()
                        .HasForeignKey("XmlFileId");

                    b.Navigation("XmlFile");
                });

            modelBuilder.Entity("ImportaXml.Models.XmlFileNaoRegistrado", b =>
                {
                    b.HasOne("ImportaXml.Models.XmlFile", "XmlFile")
                        .WithMany()
                        .HasForeignKey("XmlFileId");

                    b.Navigation("XmlFile");
                });
#pragma warning restore 612, 618
        }
    }
}
