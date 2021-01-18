using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nexos.Domain.Entities;
using Nexos.Infrastructure.Repositories;
using Nexos.PruebaTecnica.NLog;
using Nexos.PruebaTecnica.ViewModels;
using Nexos.PruebaTecnica.ViewModels.Doctores;
using Nexos.PruebaTecnica.ViewModels.Pacientes;

namespace Nexos.PruebaTecnica.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DoctoresController : BaseController<IDoctorRepository, Doctor, DoctorPostViewModel, DoctorGetViewModel>
    {
        private readonly IPacienteRepository _pacienteRepository;

        public DoctoresController(IDoctorRepository repository, IPacienteRepository pacienteRepository, ILoggerManager logger, IMapper mapper) : base(repository, logger, mapper)
        {
            _pacienteRepository = pacienteRepository;
        }

        [HttpGet("{id}/[action]")]
        public async Task<IEnumerable<PacienteGetViewModel>> GetPacientes(Guid id)
        {
            var entity = await _repository.GetAsync(id);

            var list = new List<PacienteGetViewModel>();
            if (entity != null && entity.Pacientes != null)
            {
                foreach (var item in entity.Pacientes)
                {
                    var model = _mapper.Map<Paciente, PacienteGetViewModel>(item);
                    list.Add(model);
                }
            }

            return list;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectListViewModel>> SelectList()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(x => new SelectListViewModel()
            {
                Value = x.Id.ToString(),
                Text = string.Format("{0} - {1} {2}", x.Especialidad, x.Nombre, x.Apellido)
            });
        }
    }
}
