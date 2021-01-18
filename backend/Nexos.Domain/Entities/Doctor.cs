using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nexos.Domain.Entities
{
    public class Doctor : Entity
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string CodigoProfesional { get; set; }

        [Required]
        public string Especialidad { get; set; }

        [Required]
        public string Hospital { get; set; }

        public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}
