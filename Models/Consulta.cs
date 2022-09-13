﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaAPICodeFirst.Models
{
    public class Consulta
    {
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [ForeignKey("Medico")]
        public int IdMedico { get; set; }
        public Medico Medico { get; set; }

        [ForeignKey("Paciente")]
        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; }
    }
}