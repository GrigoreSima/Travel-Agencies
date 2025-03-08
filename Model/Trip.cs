using System;

namespace Model;

[Serializable]
public class Trip : Entity<long>
{
    public string Landmark { get; }
    public string TransportCompany { get; }
    public DateTime DepartureTime { get; }
    public float Price { get; }
    public int Slots { get; set; }

    public Trip(long id, string landmark, string transportCompany, DateTime departureTime, float price, int slots) : base(id)
    {
        Landmark = landmark;
        TransportCompany = transportCompany;
        DepartureTime = departureTime;
        Price = price;
        Slots = slots;
    }

    protected bool Equals(Trip other)
    {
        return base.Equals(other) && Landmark == other.Landmark && TransportCompany == other.TransportCompany && DepartureTime.Equals(other.DepartureTime) && Price.Equals(other.Price) && Slots == other.Slots;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Trip)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (Landmark != null ? Landmark.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (TransportCompany != null ? TransportCompany.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ DepartureTime.GetHashCode();
            hashCode = (hashCode * 397) ^ Price.GetHashCode();
            hashCode = (hashCode * 397) ^ Slots;
            return hashCode;
        }
    }

    public override string ToString()
    {
        return $"{base.ToString()}, " +
               $"{nameof(Landmark)}: {Landmark}, " +
               $"{nameof(TransportCompany)}: {TransportCompany}, " +
               $"{nameof(DepartureTime)}: {DepartureTime}, " +
               $"{nameof(Price)}: {Price}, " +
               $"{nameof(Slots)}: {Slots}";
    }
    
}