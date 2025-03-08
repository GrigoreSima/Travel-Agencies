struct ThriftUser {
    1:i64 id,
    2:string username,
    3:string password
}

struct ThriftTrip {
    1:i64 id,
    2:string landmark,
    3:string transportCompany,
    4:string departureTime,
    5:double price,
    6:i32 slots,
}

struct ThriftReservation {
    1:i64 id,
    2:string clientName,
    3:string phoneNumber,
    4:i32 ticketsNo,
    5:ThriftTrip trip,
}

service IThriftService {
    bool login(1:ThriftUser user),

    void logout(1:ThriftUser user),

    list<ThriftTrip> findAllTrips(),

    list<ThriftTrip> findAllTripsForLandmarkInInterval(1:string landmark,
    2:i32 lowerLimit, 3:i32 upperLimit, 4:ThriftUser user),

    void saveReservation(1:ThriftReservation reservation)

    bool slotsModified()
}