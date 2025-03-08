using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Model;
using Persistence.Interfaces;
using Services;
using Thrift.Utils;
using ThriftCom;
using ThriftCom.Utils;

namespace ThriftServer;

public class ThriftService : IThriftService.IAsync
{
    private readonly IUserRepository _userRepository;
    private readonly ITripRepository _tripRepository;
    private readonly IReservationRepository _reservationRepository;

    private readonly IDictionary<String, IObserver> clients;

    private static readonly log4net.ILog Log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);


    public ThriftService(IUserRepository userRepository, ITripRepository tripRepository,
        IReservationRepository reservationRepository)
    {
        Log.InfoFormat("Creating Service with {0}, {1}, {2}", userRepository, tripRepository, reservationRepository);
        _userRepository = userRepository;
        _tripRepository = tripRepository;
        _reservationRepository = reservationRepository;

        clients = new Dictionary<string, IObserver>();
    }

    public Task<bool> login(ThriftUser user, CancellationToken cancellationToken = default)
    {
        Log.InfoFormat("Handling Login (user: {0}, pass: {1})", user.Username, user.Password);
        Boolean result = _userRepository.VerifyPasswordForUser(user.Username, user.Password);
        // clients[user.Username] = client;
        Log.Info(result);

        return Task.FromResult(result);
    }


    public Task logout(ThriftUser user, CancellationToken cancellationToken = default)
    {
        if (user != null)
        {
            Log.InfoFormat("Handling Logout (user: {0})", user.Username);
            if (user.Username != null) clients.Remove(user.Username);
        }

        return Task.CompletedTask;
    }

    public Task<List<ThriftTrip>> findAllTrips(CancellationToken cancellationToken = default)
    {
        Log.InfoFormat("Finding all trips");
        List<Trip> list = _tripRepository.FindAll().ToList();
        list.ForEach(trip => trip.Slots = SlotsLeft(trip));
        Log.Info(list);
        return Task.FromResult(list.ConvertAll(ConvertTrip.toThriftTrip).ToList());
    }

    public Task<List<ThriftTrip>> findAllTripsForLandmarkInInterval(string landmark, int lowerLimit, int upperLimit,
        ThriftUser user, CancellationToken cancellationToken = default)
    {
        Log.InfoFormat("Finding trips for {0}, between [{1}:00, {2}:59]", landmark,
            lowerLimit, upperLimit - 1);
        List<Trip> list = _tripRepository.FindTripsOnLocationInInterval(landmark,
            lowerLimit, upperLimit).ToList();
        list.ForEach(trip => trip.Slots = SlotsLeft(trip));
        Log.Info(list);
        return Task.FromResult(list.ConvertAll(ConvertTrip.toThriftTrip).ToList());
    }

    private Int32 SlotsLeft(Trip trip)
    {
        Log.Info("Calculating left slots");
        Int32 result = trip.Slots - _reservationRepository
            .FindReservedSlotsForTrip(trip.Id);
        Log.Info(result);
        return result;
    }

    public Task saveReservation(ThriftReservation reservation, CancellationToken cancellationToken = default)
    {
        if (reservation != null)
        {
            Log.InfoFormat("Saving reservation " +
                           "(clientName: {0}, phoneNumber: {1}, ticketsNo: {2}, trip: {3})",
                reservation.ClientName,
                reservation.PhoneNumber,
                reservation.TicketsNo,
                reservation.Trip);
            _reservationRepository.Save(
                ConvertReservation
                    .toReservation(reservation));
            // NotifySlotsUpdated();
            Log.Info(reservation);
        }

        return Task.FromResult("");
    }
    
    public Task<bool> slotsModified(CancellationToken cancellationToken = default)
    {
        // Console.WriteLine("Notify slots updated");
        //
        // foreach (var client in clients.Values)
        // {
        //     Task.Run(() => client.SlotsModified());
        // }
        //
        // return true;
        Console.WriteLine("mod");
        return Task.FromResult(false);
    }
}