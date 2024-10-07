using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormulaABD.Models
{
    [Table("Piloti")]
    public class Pilota
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Display(Name = "Nome Pilota")]
        public string Name { get; set; } = "";
    }
}