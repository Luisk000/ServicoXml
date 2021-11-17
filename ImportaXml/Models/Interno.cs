
using System.ComponentModel.DataAnnotations.Schema;

namespace ImportaXml.Models
{
    public class Interno
    {
        public int Id { get; set; }
        public string Produto { get; set; } //DB -> nfeProc_NFe_infNFe_det_prod_xProd
        public double CodProdLocal { get; set; } //catalogo
        public string Fornecedor { get; set; } //DB -> nfeProc_NFe_infNFe_emit_xNome
        public double CodFornecedorLocal { get; set; } //catalogo
        public string Empresa { get; set; } //DB -> nfeProc_NFe_infNFe_dest_xNome
        public double CodEmpresaLocal { get; set; } //catalogo
    }
}
