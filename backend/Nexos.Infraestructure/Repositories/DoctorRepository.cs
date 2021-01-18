using Microsoft.EntityFrameworkCore;
using Nexos.Domain.Contracts;
using Nexos.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Nexos.Infrastructure.Repositories
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
    }

    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(NexosDbContext context) : base(context)
        {
            _dbset = context.Doctores;
        }

        readonly DbSet<Doctor> _dbset;
        public override DbSet<Doctor> Dbset => _dbset;

        public override Task<Doctor> GetAsync(Guid id)
        {
            return Dbset.Include("Pacientes").FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
