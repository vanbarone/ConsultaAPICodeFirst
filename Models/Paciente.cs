using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using System;

namespace ConsultaAPICodeFirst.Models
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        public string Carteirinha { get; set; }

        [Required]
        public DateTime DtNascimento { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }
}
