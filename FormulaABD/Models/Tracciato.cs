using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormulaABD.Models
{
    [Table("Tracciati")]
    public class Tracciato
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Display(Name = "Nome Tracciato")]
        public string Name { get; set; } = "";
    }
}
