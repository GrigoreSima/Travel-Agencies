using System;
using System.Collections.Generic;
using Model;

namespace Services
{
    public interface IService
    {
        Boolean Login(User user, IObserver client);
        void Logout(User user);
        
        List<Trip> FindAllTrips();

        List<Trip> FindAllTripsForLandmarkInInterval(String landmark, Int32 lowerLimit,
            Int32 upperLimit, User user);
        
        void SaveReservation(Reservation reservation);
    }
}