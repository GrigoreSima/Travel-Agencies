using System;
using System.Collections.Generic;

namespace Model;

[Serializable]
public class Entity<TId>
{
    public TId Id { get; }

    public Entity(TId id)
    {
        Id = id;
    }

    protected bool Equals(Entity<TId> other)
    {
        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Entity<TId>)obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.GetHashCode(Id);
    }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}";
    }
}