using System.ComponentModel.DataAnnotations.Schema;

namespace ImportaXml.Models
{
    public class XmlFileAlgorithm
    {
        public int Id { get; set; }
        [ForeignKey("XmlFileId")]
        public XmlFile XmlFile { get; set; }
        public string XmlFileId { get; set; }
        public string nfeProc_NFe_Signature_SignedInfo_Reference_Transforms_Transform____Algorithm { get; set; } //propriedade
    }
}
