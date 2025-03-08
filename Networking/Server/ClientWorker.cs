using System;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Model;
using Networking.Requests;
using Networking.Responses;
using Services;

namespace Networking.Server
{
    public class ClientWorker : IObserver
    {
        private readonly IService _server;
        private readonly TcpClient _connection;

        private readonly NetworkStream _stream;
        private readonly IFormatter _formatter;
        private volatile bool _connected;

        public ClientWorker(IService server, TcpClient connection)
        {
            this._server = server;
            this._connection = connection;

            try
            {
                _stream = connection.GetStream();
                _formatter = new BinaryFormatter();
                _connected = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception);
            }
        }

        public virtual void Run()
        {
            while (_connected)
            {
                try
                {
                    object request = _formatter.Deserialize(_stream);
                    object response = HandleRequest((IRequest)request);
                    if (response != null)
                    {
                        SendResponse((IResponse)response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }

            try
            {
                _stream.Close();
                _connection.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception);
            }
        }

        private IResponse HandleRequest(IRequest request)
        {
            if (request is LoginRequest loginRequest)
            {
                Console.WriteLine("Login request");
                User user = loginRequest.User;
                try
                {
                    lock (_server)
                    {
                        _server.Login(user, this);
                    }

                    return new OkResponse();
                }
                catch (Exception exception)
                {
                    _connected = false;
                    return new ErrorResponse(exception.Message);
                }
            }

            if (request is LogoutRequest logoutRequest)
            {
                Console.WriteLine("Logout request");
                User user = logoutRequest.User;
                try
                {
                    lock (_server)
                    {
                        _server.Logout(user);
                    }

                    _connected = false;
                    return new OkResponse();
                }
                catch (Exception exception)
                {
                    return new ErrorResponse(exception.Message);
                }
            }

            if (request is AllTripsRequest)
            {
                Console.WriteLine("All Trips request");
                try
                {
                    Trip[] trips;
                    lock (_server)
                    {
                        trips = _server.FindAllTrips().ToArray();
                    }

                    return new AllTripsResponse(trips);
                }
                catch (Exception exception)
                {
                    return new ErrorResponse(exception.Message);
                }
            }

            if (request is FilteredTripsRequest filteredTripsRequest)
            {
                Console.WriteLine("Filtered Trips request");
                try
                {
                    Trip[] trips;
                    lock (_server)
                    {
                        trips = _server.FindAllTripsForLandmarkInInterval(
                            filteredTripsRequest.Landmark,
                            filteredTripsRequest.LowerLimit,
                            filteredTripsRequest.UpperLimit,
                            filteredTripsRequest.User
                        ).ToArray();
                    }

                    return new FilteredTripsResponse(trips);
                }
                catch (Exception exception)
                {
                    return new ErrorResponse(exception.Message);
                }
            }

            if (request is SaveReservationRequest saveReservationRequest)
            {
                Console.WriteLine("Save reservation request");
                try
                {
                    lock (_server)
                    {
                        _server.SaveReservation(saveReservationRequest.Reservation);
                    }

                    return new OkResponse();
                }
                catch (Exception exception)
                {
                    return new ErrorResponse(exception.Message);
                }
            }

            return null;
        }


        private void SendResponse(IResponse response)
        {
            Console.WriteLine("Sending response " + response);
            lock (_stream)
            {
                _formatter.Serialize(_stream, response);
                _stream.Flush();
            }
        }

        public virtual void SlotsModified()
        {
            Console.WriteLine("Slots updated !");
            try
            {
                SendResponse(new SlotsUpdatedResponse());
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception.Message);
            }
        }
    }
}