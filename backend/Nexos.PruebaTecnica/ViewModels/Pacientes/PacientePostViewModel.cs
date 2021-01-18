using System.ComponentModel.DataAnnotations;

namespace Nexos.PruebaTecnica.ViewModels.Pacientes
{
    public class PacientePostViewModel : BasePostViewModel
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string SeguroSocial { get; set; }
        
        public string CodigoPostal { get; set; }
        
        public string Telefono { get; set; }
    }
}
