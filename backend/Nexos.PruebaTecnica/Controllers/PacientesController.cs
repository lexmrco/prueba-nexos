using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class PacientesController : BaseController<IPacienteRepository, Paciente, PacientePostViewModel, PacienteGetViewModel>
    {
        IDoctorRepository _doctorRepository;
        public PacientesController(IPacienteRepository repository, IDoctorRepository doctorRepository, ILoggerManager logger, IMapper mapper) : base(repository, logger, mapper)
        {
            _doctorRepository = doctorRepository;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectListViewModel>> SelectList()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(x => new SelectListViewModel()
            {
                Value = x.Id.ToString(),
                Text = string.Format("{0} {1}", x.Nombre, x.Apellido)
            });
        }

        [HttpGet("{id}/[action]")]
        public async Task<IEnumerable<DoctorGetViewModel>> GetDoctores(string id)
        {
            var list = new List<DoctorGetViewModel>();

            if (!Guid.TryParse(id, out Guid entityId))
                return list;
            var entity = await _repository.GetAsync(entityId);

            if (entity != null && entity.Doctores != null)
            {
                foreach (var item in entity.Doctores)
                {
                    var model = _mapper.Map<Doctor, DoctorGetViewModel>(item);
                    list.Add(model);
                }
            }

            return list;
        }

        [HttpPut("[action]/{pacienteId}/{doctorId}")]
        public async Task<IActionResult> AddDoctor(string pacienteId, string doctorId)
        {

            if (!Guid.TryParse(pacienteId, out Guid pId))
                return BadRequest();

            if (!Guid.TryParse(doctorId, out Guid dId))
                return BadRequest();

            var paciente = await _repository.GetAsync(pId);

            if (paciente == null)
                return NotFound();

            try
            {
                if (!paciente.Doctores.Any(x => x.Id == dId))
                {
                    var doctor = await _doctorRepository.GetAsync(dId);
                    if (doctor == null)
                        return NotFound();

                    paciente.Doctores.Add(doctor);

                    await _repository.SaveChangesAsync();
                }
            }
            catch
            {
                _logger.LogError("Error asociando doctor paciente");
                throw;
            }

            return Ok();
        }

        [HttpPut("[action]/{pacienteId}/{doctorId}")]
        public async Task<IActionResult> RemoveDoctor(string pacienteId, string doctorId)
        {

            if (!Guid.TryParse(pacienteId, out Guid pId))
                return BadRequest();

            if (!Guid.TryParse(doctorId, out Guid dId))
                return BadRequest();

            var paciente = await _repository.GetAsync(pId);

            if (paciente == null)
                return NotFound();

            try
            {
                if (paciente.Doctores.Any(x => x.Id == dId))
                {
                    var doctor = await _doctorRepository.GetAsync(dId);
                    if (doctor == null)
                        return NotFound();
                    paciente.Doctores.Remove(doctor);
                    
                    await _repository.SaveChangesAsync();
                }
            }
            catch
            {
                _logger.LogError("Error asociando doctor paciente");
                throw;
            }

            return Ok();
        }
    }
}
