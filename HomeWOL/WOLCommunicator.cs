using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HomeUI.HomeWOL
{
    public class WOLCommunicator : IWOLCommunicator
    {

        /// <summary>
        /// The UdpClient object used for communication.
        /// </summary>
        private UdpClient Client;

        /// <summary>
        /// The port over which communication will be sent/receved.
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// An adapter class for UdpClient. Enables transmitting to a broadcast address.
        /// </summary>
        /// <param name="port">The port from which communication should be sent.</param>
        public WOLCommunicator(int port)
        {
            Port = port;
            Client = new UdpClient(Port);
            Client.EnableBroadcast = true;
        }

        /// <summary>
        /// Asynchronously sends data specified in bytes as a datagram to the recipient.
        /// </summary>
        /// <param name="bytes">The byte array to send as a payload.</param>
        /// <param name="recipient">The IPAddress that will receive the payload.</param>
        public async Task<int> SendAsync(byte[] bytes, IPAddress recipient)
        {
            var endpoint = new IPEndPoint(recipient, Port);
            return await Client.SendAsync(bytes, bytes.Length, endpoint);
        }

        /// <summary>
        /// Dispose the object, closing open sockets.
        /// </summary>
        public void Close()
        {
            Client.Close();
        }

    }
}
