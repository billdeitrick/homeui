using System.Net;
using System.Threading.Tasks;

namespace HomeWol
{
    internal interface IWolCommunicator
    {
        /// <summary>
        /// The port over which communication will be sent/receved.
        /// </summary>
        int Port { get; }

        /// <summary>
        /// Asynchronously sends data specified in bytes as a datagram to the recipient.
        /// </summary>
        /// <param name="bytes">The byte array to send as a payload.</param>
        /// <param name="recipient">The IPAddress that will receive the payload.</param>
        Task<int> SendAsync(byte[] bytes, IPAddress recipient);

        /// <summary>
        /// Dispose the object, closing open sockets.
        /// </summary>
        public void Close();
    }
}