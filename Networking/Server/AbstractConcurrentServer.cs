using System.Net.Sockets;
using System.Threading;

namespace Networking.Server
{
    public abstract class AbstractConcurrentServer : AbstractServer
    {
        protected AbstractConcurrentServer(string host, int port) : base(host, port)
        {
        }

        protected override void ProcessRequest(TcpClient client)
        {
            Thread worker = CreateWorker(client);
            worker.Start();
        }

        protected abstract Thread CreateWorker(TcpClient client);
    }
}