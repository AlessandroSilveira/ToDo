using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Domain.Entities
{
    public sealed class Entity : IEquatable<Entity>
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; private set; }

        public bool Equals(Entity other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}