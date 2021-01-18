using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexos.Domain.Entities;

namespace Nexos.Infrastructure.Mapping
{
    public class PacienteMap : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("Pacientes")
            .HasMany<Doctor>(s => s.Doctores)
            .WithMany(c => c.Pacientes).UsingEntity(j => j.ToTable("DoctoresPacientes"));
        }
    }
}
