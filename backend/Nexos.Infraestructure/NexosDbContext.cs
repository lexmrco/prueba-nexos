using Microsoft.EntityFrameworkCore;
using Nexos.Infrastructure.Mapping;
using Nexos.Domain.Entities;

namespace Nexos.Infrastructure
{
    public class NexosDbContext : DbContext
    {
        public NexosDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Doctor> Doctores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PacienteMap());
            modelBuilder.ApplyConfiguration(new DoctorMap());
        }
    }
}
