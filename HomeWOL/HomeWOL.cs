using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;

namespace HomeWol
{
    public class HomeWol
    {
        private readonly IWolCommunicator _communicator;

        /// <summary>
        /// The HomeWol class isa custom Wake-On-LAN utility.
        /// </summary>
        /// <param name="port"></param>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public HomeWol(int port) : this(new WolCommunicator(port)) { }

        /// <summary>
        /// The HomeWol class is a custom Wake-On-LAN utility.
        /// </summary>
        /// <param name="communicator">The WolCommunicator instance to be used by this HomeWol instance.</param>
        internal HomeWol(IWolCommunicator communicator) {
            _communicator = communicator;
        }

        /// <summary>
        /// Sends magic packet for specified mac to given address.
        /// </summary>
        /// <param name="mac">MAC address for which packet should be generated.</param>
        /// <param name="address">The address to which a magic packet should be sent.</param>
        /// <returns>The integer result of the UDP packe transmission.</returns>
        public async Task<int> Wake(PhysicalAddress mac, IPAddress address)
        {
            var payload = new List<byte> { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            for (int i = 0; i < 16; i++)
            {
                payload.AddRange(mac.GetAddressBytes());
            }

            return await _communicator.SendAsync(payload.ToArray(), address);
        }

        /// <summary>
        /// Sends magic packet for specified mac to given address.
        /// </summary>
        /// <param name="mac">MAC address for which packet should be generated.</param>
        /// <param name="address">The address to which a magic packet should be sent.</param>
        /// <returns>The integer result of the UDP packe transmission.</returns>
        public async Task<int> Wake(string mac, string address)
        {

            return await Wake(MacStringToPhysicalAdress(mac), IpStringToIpAddress(address));

        }

        /// <summary>
        /// Converts a MAC address string to a PhysicalAddress object.
        /// </summary>
        /// <param name="mac">MAC address (hex string, with bytes separated by colon).</param>
        /// <returns>The result of the conversion.</returns>
        public static PhysicalAddress MacStringToPhysicalAdress(string mac)
        {
            var split = mac.Split(":");

            var bytes = new byte[6];

            for (int i = 0; i < split.Length; i++)
            {
                bytes[i] = System.Convert.ToByte(split[i], 16);
            }

            return new PhysicalAddress(bytes);
        }

        /// <summary>
        /// Converts an IP address string to an IPAddress object.
        /// </summary>
        /// <param name="ip">The IPAddress in decimal, with octets separated by a ".".</param>
        /// <returns>The result of the conversion.</returns>
        public static IPAddress IpStringToIpAddress(string ip)
        {

            var split = ip.Split(".");

            var bytes = new byte[4];

            for(int i = 0; i < split.Length; i++)
            {
                bytes[i] = System.Convert.ToByte(split[i], 10);
            }

            return new IPAddress(bytes);

        }

    }
}
