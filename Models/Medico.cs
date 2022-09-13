using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaAPICodeFirst.Models
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public string CRM { get; set; }

        [ForeignKey("Especialidade")]
        public int IdEspecialidade { get; set; }
        public Especialidade Especialidade { get; set; }


        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }
}
