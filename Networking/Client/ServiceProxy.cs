using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Model;
using Networking.Requests;
using Networking.Responses;
using Services;

namespace Networking.Client;

public class ServiceProxy : IService
{
    private string host;
    private int port;

    private IObserver client;
    private TcpClient connection;

    private NetworkStream stream;
    private IFormatter formatter;

    private Queue<IResponse> responses;
    private EventWaitHandle _waitHandle;
    private volatile bool finished;

    public ServiceProxy(string host, int port)
    {
        this.host = host;
        this.port = port;
        responses = new Queue<IResponse>();
    }

    private void InitializeConnection()
    {
        try
        {
            connection = new TcpClient(host, port);
            stream = connection.GetStream();
            formatter = new BinaryFormatter();
            finished = false;
            _waitHandle = new AutoResetEvent(false);
            StartReader();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void CloseConnection()
    {
        finished = true;
        try
        {
            stream.Close();

            connection.Close();
            _waitHandle.Close();
            client = null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void SendRequest(IRequest request)
    {
        try
        {
            formatter.Serialize(stream, request);
            stream.Flush();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private IResponse ReadResponse()
    {
        IResponse response = null;
        try
        {
            _waitHandle.WaitOne();
            lock (responses)
            {
                response = responses.Dequeue();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return response;
    }

    private void HandleUpdate(IUpdateResponse update)
    {
        if (update is SlotsUpdatedResponse)
        {
            Console.WriteLine("Slots updated !");
            try
            {
                client.SlotsModified();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    private void StartReader()
    {
        Thread tw = new Thread(Run);
        tw.Start();
    }

    public virtual void Run()
    {
        while (!finished)
        {
            try
            {
                object response = formatter.Deserialize(stream);
                Console.WriteLine("Response received " + response);
                if (response is IUpdateResponse updateResponse)
                {
                    HandleUpdate(updateResponse);
                }
                else
                {
                    lock (responses)
                    {
                        responses.Enqueue((IResponse)response);
                    }

                    _waitHandle.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Reading error " + e);
            }
        }
    }

    public bool Login(User user, IObserver observer)
    {
        InitializeConnection();

        SendRequest(new LoginRequest(user));
        IResponse response = ReadResponse();

        if (response is OkResponse)
        {
            client = observer;
            return true;
        }

        if (response is ErrorResponse error)
        {
            CloseConnection();
            Console.WriteLine("Exception: " + error);
            return false;
        }

        return false;
    }

    public void Logout(User user)
    {
        SendRequest(new LogoutRequest(user));
        ReadResponse();
        CloseConnection();
        Console.WriteLine("Disconnected !");
    }

    public List<Trip> FindAllTrips()
    {
        SendRequest(new AllTripsRequest());
        IResponse response = ReadResponse();

        if (response is ErrorResponse error)
        {
            Console.WriteLine("Exception: " + error);
            return new List<Trip>();
        }

        return ((AllTripsResponse)response).Trips.ToList();
    }

    public List<Trip> FindAllTripsForLandmarkInInterval(string landmark, int lowerLimit, int upperLimit, User user)
    {
        SendRequest(new FilteredTripsRequest(landmark, lowerLimit, upperLimit, user));
        IResponse response = ReadResponse();

        if (response is ErrorResponse error)
        {
            Console.WriteLine("Exception: " + error);
            return new List<Trip>();
        }

        return ((FilteredTripsResponse)response).Trips.ToList();
    }

    public void SaveReservation(Reservation reservation)
    {
        SendRequest(new SaveReservationRequest(reservation));
        IResponse response = ReadResponse();

        if (response is ErrorResponse error)
        {
            Console.WriteLine("Exception: " + error);
        }
    }
}