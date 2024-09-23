using System.ComponentModel.DataAnnotations.Schema;

namespace FormulaABD.Models
{
    [Table("Piloti")]
    public class Pilota
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "";
    }
}
