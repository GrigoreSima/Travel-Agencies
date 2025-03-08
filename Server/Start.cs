using System;
using System.Collections.Generic;
using System.Configuration;
using Networking.Server;
using Persistence;
using Persistence.Interfaces;
using Services;

namespace Server;

public class Start
{
    static public void Main(String[] args)
    {
        Console.WriteLine("Reading properties from app.config ...");
        int port;
        String portConfig = ConfigurationManager.AppSettings["port"];
        if (portConfig == null)
        {
            Console.WriteLine("Port property not set.");
            return;
        }
        else
        {
            bool result = Int32.TryParse(portConfig, out port);
            if (!result)
            {
                Console.WriteLine("Port property not a number.");
                return;
            }
        }

        String ip = ConfigurationManager.AppSettings["ip"];

        if (ip == null)
        {
            Console.WriteLine("Port property not set.");
            return;
        }

        Console.WriteLine("Using  server on IP {0} and port {1}", ip, port);

        Console.WriteLine("Configuration Settings for tasksDB {0}", GetConnectionStringByName("database"));
        IDictionary<String, string> props = new SortedList<String, String>();
        props.Add("ConnectionString", GetConnectionStringByName("database"));

        IUserRepository userRepository = new UserRepository(props);
        ITripRepository tripRepository = new TripRepository(props);
        IReservationRepository reservationRepository = new ReservationRepository(props);

        IService service = new Service(userRepository, tripRepository, reservationRepository);


        Console.WriteLine("Starting server on IP {0} and port {1}", ip, port);
        AbstractServer server = new ConcurrentServer(ip, port, service);
        server.Start();
        Console.WriteLine("Server started ...");
    }

    static string GetConnectionStringByName(string name)
    {
        string returnValue = null;

        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];

        if (settings != null)
            returnValue = settings.ConnectionString;

        return returnValue;
    }
}