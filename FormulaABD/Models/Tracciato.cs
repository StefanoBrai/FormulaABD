using System.ComponentModel.DataAnnotations.Schema;

namespace FormulaABD.Models
{
    [Table("Tracciati")]
    public class Tracciato
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "";
    }
}
