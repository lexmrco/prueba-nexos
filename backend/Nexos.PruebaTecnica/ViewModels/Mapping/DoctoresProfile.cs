using AutoMapper;
using Nexos.Domain.Entities;
using Nexos.PruebaTecnica.ViewModels.Doctores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nexos.PruebaTecnica.ViewModels.Mapping
{
    public class DoctoresProfile : Profile
    {
        public DoctoresProfile()
        {
            CreateMap<Doctor, DoctorGetViewModel>();
            CreateMap<DoctorGetViewModel, Doctor>();
            CreateMap<Doctor, DoctorPostViewModel>();
            CreateMap<DoctorPostViewModel, Doctor>();
        }
    }
}
