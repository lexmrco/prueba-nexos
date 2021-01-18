using Microsoft.EntityFrameworkCore;
using Nexos.Domain.Contracts;
using Nexos.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Nexos.Infrastructure.Repositories
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
    }

    public class PacienteRepository : Repository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(NexosDbContext context) : base(context)
        {
            _dbset = context.Pacientes;
        }

        readonly DbSet<Paciente> _dbset;
        public override DbSet<Paciente> Dbset => _dbset;

        public override Task<Paciente> GetAsync(Guid id)
        {
            return Dbset.Include("Doctores").FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
