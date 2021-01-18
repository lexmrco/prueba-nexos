using AutoMapper;
using Nexos.Domain.Entities;
using Nexos.PruebaTecnica.ViewModels.Pacientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nexos.PruebaTecnica.ViewModels.Mapping
{
    public class PacientesProfile : Profile
    {
        public PacientesProfile()
        {
            CreateMap<Paciente, PacienteGetViewModel>();
            CreateMap<PacienteGetViewModel, Paciente>();
            CreateMap<Paciente, PacientePostViewModel>();
            CreateMap<PacientePostViewModel, Paciente>();
        }
    }
}
