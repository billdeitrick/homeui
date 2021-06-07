using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HomeWol
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal class WolCommunicator : IWolCommunicator
    {

        /// <summary>
        /// The UdpClient object used for communication.
        /// </summary>
        private readonly UdpClient _client;

        /// <summary>
        /// The port over which communication will be sent/receved.
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// An adapter class for UdpClient. Enables transmitting to a broadcast address.
        /// </summary>
        /// <param name="port">The port from which communication should be sent.</param>
        public WolCommunicator(int port)
        {
            Port = port;
            _client = new UdpClient(Port)
            {
                EnableBroadcast = true
            };
        }

        /// <summary>
        /// Asynchronously sends data specified in bytes as a datagram to the recipient.
        /// </summary>
        /// <param name="bytes">The byte array to send as a payload.</param>
        /// <param name="recipient">The IPAddress that will receive the payload.</param>
        public async Task<int> SendAsync(byte[] bytes, IPAddress recipient)
        {
            var endpoint = new IPEndPoint(recipient, Port);
            return await _client.SendAsync(bytes, bytes.Length, endpoint);
        }

        /// <summary>
        /// Dispose the object, closing open sockets.
        /// </summary>
        public void Close()
        {
            _client.Close();
        }

    }
}
