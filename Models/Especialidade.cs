using System.ComponentModel.DataAnnotations;

namespace ConsultaAPICodeFirst.Models
{
    public class Especialidade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Categoria { get; set; }
    }
}
