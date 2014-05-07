using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DictionaryClient
{
    internal class ServerGateway
    {
        private const int BufferLength = 1024;

        private readonly byte[] _buffer;
        private readonly Action<Socket> _connect;

        public ServerGateway(string address, int port)
        {
            IPAddress ip;
            if (IPAddress.TryParse(address, out ip))
            {
                var ipep = new IPEndPoint(ip, port);
                _connect = x => x.Connect(ipep);
            }
            else
            {
                _connect = x => x.Connect(address, port);
            }

            _buffer = new byte[BufferLength];
        }

        public Responce Request(string request)
        {
            using (var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    _connect(server);
                }
                catch (SocketException e)
                {
                    return Responce.CreateError(e.ToString());
                }
                var requestBuffer = Encoding.ASCII.GetBytes(request);
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