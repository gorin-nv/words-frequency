using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DictionaryClient
{
    internal class ServerGateway
    {
        private const int BufferLength = 1024;
        private readonly byte[] _buffer;
        private readonly IPEndPoint _ipep;

        public ServerGateway(string ip, int port)
        {
            _ipep = new IPEndPoint(IPAddress.Parse(ip), port);
            _buffer = new byte[BufferLength];
        }

        public Responce Request(string request)
        {
            var requestBuffer = Encoding.ASCII.GetBytes(request);
            using (var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    server.Connect(_ipep);
                }
                catch (SocketException e)
                {
                    return Responce.CreateError(e.ToString());
                }
                server.Send(requestBuffer);
                var responce = server.Receive(_buffer);
                var data = Encoding.ASCII.GetString(_buffer, 0, responce);
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                return Responce.Create(data);
            }
        }
    }
}