using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nexos.Domain.Entities
{
    public class Paciente : Entity
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }
        
        [Required]
        public string SeguroSocial { get; set; }
        
        public string CodigoPostal { get; set; }
        
        public string Telefono { get; set; }
        
        public virtual ICollection<Doctor> Doctores { get; set; }
    }
}
