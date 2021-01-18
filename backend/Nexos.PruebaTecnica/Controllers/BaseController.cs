using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nexos.Domain;
using Nexos.Domain.Contracts;
using Nexos.PruebaTecnica.NLog;
using Nexos.PruebaTecnica.ViewModels;

namespace Nexos.PruebaTecnica.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public abstract class BaseController<TRepository, TEntity, TPostViewModel, TGetViewModel> : ControllerBase
        where TEntity: Entity
        where TRepository: IRepository<TEntity>
        where TPostViewModel: BasePostViewModel
        where TGetViewModel: BaseGetViewModel
    {
        protected readonly TRepository _repository;
        
        protected readonly IMapper _mapper;
        
        protected readonly ILoggerManager _logger;

        public BaseController(TRepository repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<TGetViewModel>> Get()
        {
            var list = new List<TGetViewModel>();
            foreach (var entity in await _repository.GetAllAsync())
            {
                var model = MapEntityToGetViewModel(entity);
                list.Add(model);
            }
            return list;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TGetViewModel>> Get(string id)
        {
            if (!Guid.TryParse(id, out Guid entityId))
                return BadRequest();
            //LogReader();
            var entity = await _repository.GetAsync(entityId);

            if (entity == null)
            {
                return NotFound();
            }

            return MapEntityToGetViewModel(entity);
        }

        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TPostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInfo(ModelState);
                return BadRequest("Datos no válidos");
            }

            TEntity entity = Activator.CreateInstance<TEntity>();
            entity = MapPostViewModelToEntity(model, entity);
            
            try
            {
                _repository.Add(entity);
                await _repository.SaveChangesAsync();
            }
            catch
            {
                _logger.LogError("Error creando datos");
                _logger.LogError(model);
                throw;
            }

            return CreatedAtAction("Get", new { id = entity.Id }, MapEntityToGetViewModel(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, TPostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Datos no válidos");
            }

            if (!Guid.TryParse(id, out Guid entityId))
                return BadRequest();

            var entity = await _repository.GetAsync(entityId);

            if (entity == null)
                return NotFound();

            entity = MapPostViewModelToEntity(model, entity);

            try
            {
                _repository.Update(entity);
                await _repository.SaveChangesAsync();
            }
            catch
            {
                _logger.LogError("Error actualizando datos");
                _logger.LogError(entity);
                throw;
            }

            return Ok(MapEntityToGetViewModel(entity));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (!Guid.TryParse(id, out Guid entityId))
                return BadRequest();
            var entity = await _repository.GetAsync(entityId);
            if (entity == null)
            {
                return NotFound();
            }

            try
            {
                _repository.Delete(entity);
                await _repository.SaveChangesAsync();

            }
            catch (Exception)
            {
                _logger.LogError("Error eliminando datos");
                _logger.LogError(entity);
                throw;
            }

            return Ok();
        }

        private TGetViewModel MapEntityToGetViewModel(TEntity entity)
        {
            return _mapper.Map<TGetViewModel>(entity);
        }

        private TEntity MapPostViewModelToEntity(TPostViewModel model, TEntity entity)
        {
            return _mapper.Map<TPostViewModel, TEntity>(model, entity);
        }
    }
}
