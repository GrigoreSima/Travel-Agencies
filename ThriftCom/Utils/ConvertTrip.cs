using System;
using Model;
using ThriftCom;

namespace Thrift.Utils;

public class ConvertTrip
{
    public static Trip toTrip(ThriftTrip trip)
    {
        return new Trip(
            trip.Id,
            trip.Landmark,
            trip.TransportCompany,
            DateTime.ParseExact(trip.DepartureTime, "dd.MM.yyyy HH:mm:ss", null),
            (float) trip.Price,
            trip.Slots
        );
    }
    
    public static ThriftTrip toThriftTrip(Trip trip)
    {
        ThriftTrip thriftTrip = new ThriftTrip();
        thriftTrip.Id = trip.Id;
        thriftTrip.Landmark = trip.Landmark;
        thriftTrip.TransportCompany = trip.TransportCompany;
        thriftTrip.DepartureTime = trip.DepartureTime.ToString("dd.MM.yyyy HH:mm:ss");
        thriftTrip.Price = trip.Price;
        thriftTrip.Slots = trip.Slots;

        return thriftTrip;
    }
}