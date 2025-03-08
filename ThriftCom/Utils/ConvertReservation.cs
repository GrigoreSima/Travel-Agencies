using Model;
using Thrift.Utils;
using ThriftCom;

namespace ThriftCom.Utils;

public class ConvertReservation
{
    public static Reservation toReservation(ThriftReservation reservation)
    {
        return new Reservation(
            reservation.Id,
            reservation.ClientName,
            reservation.PhoneNumber,
            reservation.TicketsNo,
            ConvertTrip.toTrip(reservation.Trip)
        );
    }
    
    public static ThriftReservation toThriftReservation(Reservation reservation)
    {
        ThriftReservation thriftReservation = new ThriftReservation();
        thriftReservation.Id = reservation.Id;
        thriftReservation.ClientName = reservation.ClientName;
        thriftReservation.PhoneNumber = reservation.PhoneNumber;
        thriftReservation.TicketsNo = reservation.TicketsNo;
        thriftReservation.Trip = ConvertTrip.toThriftTrip(reservation.Trip);

        return thriftReservation;
    }
}