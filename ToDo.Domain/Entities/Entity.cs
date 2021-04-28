using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Domain.Entities
{
    public class Entity : IEquatable<Entity>
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; private set; }

        public bool Equals(Entity other)
        {
            return Id == other.Id;
        }
    }
}