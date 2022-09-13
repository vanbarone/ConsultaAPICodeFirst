using System.ComponentModel.DataAnnotations;

namespace ConsultaAPICodeFirst.Models
{
    public class TipoUsuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Tipo { get; set; }
    }
}
