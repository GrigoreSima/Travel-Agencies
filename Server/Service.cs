using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using Persistence.Interfaces;
using Services;

namespace Server;

public class Service : IService
{
    private readonly IUserRepository _userRepository;
    private readonly ITripRepository _tripRepository;
    private readonly IReservationRepository _reservationRepository;

    private readonly IDictionary<String, IObserver> clients;

    private static readonly log4net.ILog Log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);


    public Service(IUserRepository userRepository, ITripRepository tripRepository,
        IReservationRepository reservationRepository)
    {
        Log.InfoFormat("Creating Service with {0}, {1}, {2}", userRepository, tripRepository, reservationRepository);
        _userRepository = userRepository;
        _tripRepository = tripRepository;
        _reservationRepository = reservationRepository;

        clients = new Dictionary<string, IObserver>();
    }
    
    private void NotifySlotsUpdated(){
        Console.WriteLine("Notify slots updated");
        
        foreach (var client in clients.Values)
        {
            Task.Run(() => client.SlotsModified());
        }

    }

    public Boolean Login(User user, IObserver client)
    {
        Log.InfoFormat("Handling Login (user: {0}, pass: {1})", user.Username, user.Password);
        Boolean result = _userRepository.VerifyPasswordForUser(user.Username, user.Password);
        clients[user.Username] = client;
        Log.Info(result);
        return result;
    }

    public void Logout(User user)
    {
        Log.InfoFormat("Handling Logout (user: {0}, pass: {1})", user.Username);
        clients.Remove(user.Username);
    }

    public List<Trip> FindAllTrips()
    {
        Log.InfoFormat("Finding all trips");
        List<Trip> list = _tripRepository.FindAll().ToList();
        list.ForEach(trip => trip.Slots = SlotsLeft(trip));
        Log.Info(list);
        return list;
    }

    public List<Trip> FindAllTripsForLandmarkInInterval(String landmark, Int32 lowerLimit,
        Int32 upperLimit, User user)
    {
        Log.InfoFormat("Finding trips for {0}, between [{1}:00, {2}:59]", landmark,
            lowerLimit, upperLimit - 1);
        List<Trip> list = _tripRepository.FindTripsOnLocationInInterval(landmark,
            lowerLimit, upperLimit).ToList();
        list.ForEach(trip => trip.Slots = SlotsLeft(trip));
        Log.Info(list);
        return list;
    }

    private Int32 SlotsLeft(Trip trip)
    {
        Log.Info("Calculating left slots");
        Int32 result = trip.Slots - _reservationRepository
            .FindReservedSlotsForTrip(trip.Id);
        Log.Info(result);
        return result;
    }

    public void SaveReservation(Reservation reservation)
    {
        Log.InfoFormat("Saving reservation " +
                       "(clientName: {0}, phoneNumber: {1}, ticketsNo: {2}, trip: {3})",
            reservation.ClientName,
            reservation.PhoneNumber,
            reservation.TicketsNo,
            reservation.Trip);
        _reservationRepository.Save(reservation);
        NotifySlotsUpdated();
        Log.Info(reservation);
    }
}