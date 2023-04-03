namespace Domain.Entities
{
    public abstract class Entity
    {
        protected Entity(Guid id) => Id = id;
        protected Entity() { }
        public Guid Id { get; init; }

        public static bool operator !=(Entity left, Entity right) => !(left == right);

        public static bool operator ==(Entity left, Entity right) => left?.Equals(right) == true;

        public override bool Equals(object obj) => obj is not null and Entity && (ReferenceEquals(this, obj) || Id == (obj as Entity).Id);

        public override int GetHashCode() => Id.GetHashCode();
    }
}