using System;
using System.Collections.Generic;
using System.Data;
using Model;
using Persistence.Interfaces;
using Persistence.Utils;

namespace Persistence;

public class TripRepository : ITripRepository
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    IDictionary<String, string> props;

    public TripRepository(IDictionary<string, string> props)
    {
        Log.Info("Creating TripRepository");
        this.props = props;
    }

    public Trip FindOne(long id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Trip> FindAll()
    {
        Log.Info("Entering FindAll");

        IDbConnection connection = DbUtils.GetConnection(props);
        IList<Trip> trips = new List<Trip>();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT id, landmark, transportCompany, departureTime, price, slots FROM Trips";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    long id = reader.GetInt64(0);
                    String landmark = reader.GetString(1);
                    String transportCompany = reader.GetString(2);
                    DateTime departureTime = DateTime.ParseExact(reader.GetString(3), "dd.MM.yyyy HH:mm:ss", null);
                    float price = reader.GetFloat(4);
                    int slots = reader.GetInt32(5);
                 
                    trips.Add(new Trip(
                            id, 
                            landmark,
                            transportCompany,
                            departureTime,
                            price,
                            slots
                        ));
                }
            }
        }

        Log.InfoFormat("Exiting FindAll with result: \n{0}", trips);
        return trips;
    }

    public Boolean Save(Trip entity)
    {
        throw new NotImplementedException();
    }

    public Boolean Delete(long id)
    {
        throw new NotImplementedException();
    }

    public Boolean Update(Trip entity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Trip> FindTripsOnLocationInInterval(string landmark, int lowerLimit, int upperLimit)
    {
        Log.InfoFormat("Entering FindTripsOnLocationInInterval with landmark: {0}, interval=[{1}, {2})",
            landmark, lowerLimit, upperLimit);

        IDbConnection connection = DbUtils.GetConnection(props);
        IList<Trip> trips = new List<Trip>();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT id, transportCompany, departureTime, price, slots FROM Trips" +
                                  " WHERE landmark=@landmark";
            
            IDbDataParameter landmarkParam = command.CreateParameter();
            landmarkParam.ParameterName = "@landmark";
            landmarkParam.Value = landmark;
            command.Parameters.Add(landmarkParam);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    long id = reader.GetInt64(0);
                    String transportCompany = reader.GetString(1);
                    DateTime departureTime = DateTime.ParseExact(reader.GetString(2), "dd.MM.yyyy HH:mm:ss", null);
                    float price = reader.GetFloat(3);
                    int slots = reader.GetInt32(4);
                    
                    if(departureTime.Hour < lowerLimit || departureTime.Hour >= upperLimit)
                        continue;
                 
                    trips.Add(new Trip(
                        id, 
                        landmark,
                        transportCompany,
                        departureTime,
                        price,
                        slots
                    ));
                }
            }
        }
        
        Log.InfoFormat("Exiting FindTripsOnLocationInInterval with result: \n{0})", trips);
        return trips;
    }
}