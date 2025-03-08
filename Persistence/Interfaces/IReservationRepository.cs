using Model;

namespace Persistence.Interfaces;

public interface IReservationRepository : IRepository<long, Reservation>
{
    /// Returns the number of slots reserved for the trip with the specified tripID
    /// <param name="tripId">id for the wanted trip</param>
    /// <returns>
    /// the number of slots reserved for that trip
    /// </returns>
    int FindReservedSlotsForTrip(long tripId);
}