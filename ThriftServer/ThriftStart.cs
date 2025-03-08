using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Persistence;
using Persistence.Interfaces;
using Thrift;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;
using Thrift.Transport.Server;
using ThriftCom;

namespace ThriftServer;

public class ThriftStart
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

        ThriftService service = new ThriftService(userRepository, tripRepository, reservationRepository);
        IThriftService.AsyncProcessor processor = new IThriftService.AsyncProcessor(service);

        IPAddress adr = IPAddress.Parse(ip);
        IPEndPoint ep = new IPEndPoint(adr, port);
        TcpListener tcpListener = new TcpListener(ep);

        Console.WriteLine("Starting server on IP {0} and port {1}", ip, port);

        TServerTransport serverTransport = new TServerSocketTransport(tcpListener, new TConfiguration());
        var protocol = new TBinaryProtocol.Factory();
        // TServer server = new TSimpleAsyncServer(processor, serverTransport, protocol, protocol, new LoggerFactory());
        // server.ServeAsync(CancellationToken.None).Wait();
        
        TServer server = new TThreadPoolAsyncServer(processor, serverTransport, new TBufferedTransport.Factory(), protocol);
        server.ServeAsync(CancellationToken.None).GetAwaiter().GetResult();
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