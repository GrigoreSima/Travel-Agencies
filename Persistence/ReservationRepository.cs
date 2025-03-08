using System;
using System.Collections.Generic;
using System.Data;
using Model;
using Persistence.Interfaces;
using Persistence.Utils;

namespace Persistence;

public class ReservationRepository : IReservationRepository
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    readonly IDictionary<String, string> _props;

    public ReservationRepository(IDictionary<string, string> props)
    {
        Log.Info("Creating ReservationRepository");
        this._props = props;
    }

    public Reservation FindOne(long id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Reservation> FindAll()
    {
        throw new NotImplementedException();
    }

    public Boolean Save(Reservation entity)
    {
        Log.InfoFormat("Entering Save with reservation {0}", entity);
        var connection = DbUtils.GetConnection(_props);

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO Reservations " +
                                  "(clientName, phoneNumber, ticketsNo, tripID) " +
                                  "VALUES (@clientName, @phoneNumber, @ticketsNo, @tripID)";
            
            var clientNameParam = command.CreateParameter();
            clientNameParam.ParameterName = "@clientName";
            clientNameParam.Value = entity.ClientName;
            command.Parameters.Add(clientNameParam);
            
            var phoneNumberParam = command.CreateParameter();
            phoneNumberParam.ParameterName = "@phoneNumber";
            phoneNumberParam.Value = entity.PhoneNumber;
            command.Parameters.Add(phoneNumberParam);
            
            var ticketsNoParam = command.CreateParameter();
            ticketsNoParam.ParameterName = "@ticketsNo";
            ticketsNoParam.Value = entity.TicketsNo;
            command.Parameters.Add(ticketsNoParam);
            
            var tripIdParam = command.CreateParameter();
            tripIdParam.ParameterName = "@tripID";
            tripIdParam.Value = entity.Trip.Id;
            command.Parameters.Add(tripIdParam);

            Boolean result = command.ExecuteNonQuery() != 0;
            Log.InfoFormat("Exiting saving reservation with value {0}", result);
            return result;
        }
    }

    public Boolean Delete(long id)
    {
        throw new NotImplementedException();
    }

    public Boolean Update(Reservation entity)
    {
        throw new NotImplementedException();
    }

    public int FindReservedSlotsForTrip(long tripId)
    {
        Log.InfoFormat("Entering FindReservedSlotsForTrip for tripID {0}", tripId);

        IDbConnection connection = DbUtils.GetConnection(_props);
        int slotsReserved = 0;
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT ticketsNo FROM Reservations WHERE tripID = @tripID";
            
            IDbDataParameter tripIdParam = command.CreateParameter();
            tripIdParam.ParameterName = "@tripID";
            tripIdParam.Value = tripId;
            command.Parameters.Add(tripIdParam);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    slotsReserved += reader.GetInt32(0);
                }
            }
        }

        Log.InfoFormat("Exiting FindReservedSlotsForTrip with value {0}", slotsReserved);

        return slotsReserved;
    }
}