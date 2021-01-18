using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexos.Domain.Entities;
using System;

namespace Nexos.Infrastructure.Mapping
{
    public class DoctorMap : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctores").HasMany<Paciente>(s => s.Pacientes)
            .WithMany(c => c.Doctores).UsingEntity(j => j.ToTable("DoctoresPacientes"));
            //InitialData(builder);
        }

        private void InitialData(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasData(new Doctor
            {
                Id = Guid.NewGuid(),
                Nombre = "Stephen",
                Apellido = "Strange",
            }, new Doctor
            {
                Id = Guid.NewGuid(),
                Nombre = "Gragory",
                Apellido = "House"
            });
        }
    }
}
