using System;
using System.Net;
using System.Net.Sockets;

namespace Networking.Server
{
    public abstract class AbstractServer
    {
        private TcpListener _server;
        private readonly String _host;
        private readonly Int32 _port;

        protected AbstractServer(string host, int port)
        {
            this._host = host;
            this._port = port;
        }

        public void Start()
        {
            IPAddress adr = IPAddress.Parse(_host);
            IPEndPoint ep=new IPEndPoint(adr,_port);
            _server=new TcpListener(ep);
            _server.Start();
            while (true)
            {
                Console.WriteLine("Waiting for clients ...");
                TcpClient client = _server.AcceptTcpClient();
                Console.WriteLine("Client connected ...");
                ProcessRequest(client);
            }
        }

        protected abstract void ProcessRequest(TcpClient client);
    }
}