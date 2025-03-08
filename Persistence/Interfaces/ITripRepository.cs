using System;
using System.Collections.Generic;
using Model;

namespace Persistence.Interfaces;

public interface ITripRepository : IRepository<long, Trip>
{
    /// Returns all the trips for the specified landmark in the interval of time of [lowerLimit:00, upperLimit:00)
    /// <param name="landmark">the wanted landmark</param>
    /// <param name="lowerLimit">the lower limit of the interval (as hour)</param>
    /// <param name="upperLimit">the upper limit of the interval (as hour)</param>
    /// <returns>
    /// an IEnumerable encapsulating all the entities for that landmark in that interval
    /// </returns>
    IEnumerable<Trip> FindTripsOnLocationInInterval(String landmark, int lowerLimit, int upperLimit);
}