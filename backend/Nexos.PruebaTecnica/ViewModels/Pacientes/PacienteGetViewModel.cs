
namespace Nexos.PruebaTecnica.ViewModels.Pacientes
{
    public class PacienteGetViewModel : BaseGetViewModel
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string SeguroSocial { get; set; }
        
        public string CodigoPostal { get; set; }
        
        public string Telefono { get; set; }
    }
}
