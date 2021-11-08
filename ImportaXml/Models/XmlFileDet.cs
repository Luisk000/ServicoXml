using System.ComponentModel.DataAnnotations.Schema;

namespace ImportaXml.Models
{
    public class XmlFileDet
    {
        public int Id { get; set; }
        [ForeignKey("XmlFileId")]
        public XmlFile XmlFile { get; set; }
        public string XmlFileId { get; set; }
        public string nfeProc_NFe_infNFe_det____nItem { get; set; } //propriedade

        public string nfeProc_NFe_infNFe_det_prod_cProd { get; set; }
        public string nfeProc_NFe_infNFe_det_prod_cEAN { get; set; }
        public string nfeProc_NFe_infNFe_det_prod_xProd { get; set; }
        public string nfeProc_NFe_infNFe_det_prod_NCM { get; set; }
        public string nfeProc_NFe_infNFe_det_prod_CFOP { get; set; }
        public string nfeProc_NFe_infNFe_det_prod_uCom { get; set; }
        public string nfeProc_NFe_infNFe_det_prod_qCom { get; set; }
        public string nfeProc_NFe_infNFe_det_prod_vUnCom { get; set; }
        public string nfeProc_NFe_infNFe_det_prod_vProd { get; set; }

        public string nfeProc_NFe_infNFe_det_prod_cEANTrib { get; set; }
        public string nfeProc_NFe_infNFe_det_prod_uTrib { get; set; }
        public string nfeProc_NFe_infNFe_det_prod_qTrib { get; set; }
        public string nfeProc_NFe_infNFe_det_prod_vUnTrib { get; set; }
        public string nfeProc_NFe_infNFe_det_prod_indTot { get; set; }

        public string nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMSN102_orig { get; set; }
        public string nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMSN102_CSOSN { get; set; }

        public string nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_orig { get; set; }
        public string nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_CST { get; set; }

        public string nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_modBC { get; set; }

        public string nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_vBC { get; set; }

        public string nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_pICMS { get; set; }

        public string nfeProc_NFe_infNFe_det_Imposto_ICMS_ICMS00_vICMS { get; set; }
        public string nfeProc_NFe_infNFe_det_Imposto_IPI_cEnq { get; set; }
        public string nfeProc_NFe_infNFe_det_Imposto_IPI_IPINT_CST { get; set; }

        public string nfeProc_NFe_infNFe_det_Imposto_IPI_IPINTrib_CST { get; set; }
        public string nfeProc_NFe_infNFe_det_Imposto_IPI_IPINTrib_vBC { get; set; }
        public string nfeProc_NFe_infNFe_det_Imposto_IPI_IPINTrib_pIPI { get; set; }
        public string nfeProc_NFe_infNFe_det_Imposto_IPI_IPINTrib_vIPI { get; set; }


        public string nfeProc_NFe_infNFe_det_Imposto_PIS_PISNT_CST { get; set; }

        public string nfeProc_NFe_infNFe_det_Imposto_PIS_PISAliq_CST { get; set; }
        public string nfeProc_NFe_infNFe_det_Imposto_PIS_PISAliq_vBC { get; set; }
        public string nfeProc_NFe_infNFe_det_Imposto_PIS_PISAliq_pPIS { get; set; }
        public string nfeProc_NFe_infNFe_det_Imposto_PIS_PISAliq_vPIS { get; set; }


        public string nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSNT_CST { get; set; }

        public string nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_CST { get; set; }
        public string nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_vBC { get; set; }
        public string nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_pCOFINS { get; set; }
        public string nfeProc_NFe_infNFe_det_Imposto_COFINS_COFINSAliq_vCOFINS { get; set; }
    }
}
