using System.ComponentModel.DataAnnotations.Schema;

namespace ImportaXml.Models
{
    public class XmlFileNaoRegistrado
    {
        public int Id { get; set; }
        [ForeignKey("XmlFileId")]
        public XmlFile XmlFile { get; set; }
        public string XmlFileId { get; set; }
        public string InfoNaoRegistrada { get; set; }
    }
}
