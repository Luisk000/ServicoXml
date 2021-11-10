using System.ComponentModel.DataAnnotations;

namespace SerilogConfiguration.Models
{
    public class LogLevel
    {
        public int Id { get; set; }
        [MinLength(1)]
        [MaxLength(5)]
        public int Level { get; set; }
    }
}
