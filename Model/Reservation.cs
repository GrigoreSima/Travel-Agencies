using System;

namespace Model;

[Serializable]
public class Reservation : Entity<long>
{
    public string ClientName { get; }
    public string PhoneNumber { get; }
    public int TicketsNo { get; }
    public Trip Trip { get; }

    public Reservation(long id, string clientName, string phoneNumber, int ticketsNo, Trip trip) : base(id)
    {
        ClientName = clientName;
        PhoneNumber = phoneNumber;
        TicketsNo = ticketsNo;
        Trip = trip;
    }

    protected bool Equals(Reservation other)
    {
        return base.Equals(other) && ClientName == other.ClientName && PhoneNumber == other.PhoneNumber && TicketsNo == other.TicketsNo && Equals(Trip, other.Trip);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Reservation)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (ClientName != null ? ClientName.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (PhoneNumber != null ? PhoneNumber.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ TicketsNo;
            hashCode = (hashCode * 397) ^ (Trip != null ? Trip.GetHashCode() : 0);
            return hashCode;
        }
    }

    public override string ToString()
    {
        return $"{base.ToString()}, " +
               $"{nameof(ClientName)}: {ClientName}, " +
               $"{nameof(PhoneNumber)}: {PhoneNumber}, " +
               $"{nameof(TicketsNo)}: {TicketsNo}, " +
               $"{nameof(Trip)}: {Trip}";
    }
}