using System;
using System.ComponentModel.DataAnnotations;

namespace Nexos.Domain
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
