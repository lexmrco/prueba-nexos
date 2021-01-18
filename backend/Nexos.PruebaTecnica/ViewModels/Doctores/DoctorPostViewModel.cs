using System.ComponentModel.DataAnnotations;

namespace Nexos.PruebaTecnica.ViewModels.Doctores
{
    public class DoctorPostViewModel : BasePostViewModel
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
    }
}
