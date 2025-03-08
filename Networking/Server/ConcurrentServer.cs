using System;
using System.Net.Sockets;
using System.Threading;
using Services;

namespace Networking.Server
{
    public class ConcurrentServer : AbstractConcurrentServer
    {
        private IService server;
        private ClientWorker worker;

        public ConcurrentServer(string host, int port, IService server) : base(host, port)
        {
            this.server = server;
            Console.WriteLine($"Starting server on {host}:{port}");
        }

        protected override Thread CreateWorker(TcpClient client)
        {
            worker = new ClientWorker(server, client);
            return new Thread(worker.Run);
        }
    }
}