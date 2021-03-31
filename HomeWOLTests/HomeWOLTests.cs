using Xunit;
using HomeUI.HomeWOL;
using Moq;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Net;

namespace HomeWOLTests
{
    public class HomeWOLTests
    {

        [Fact]
        public void HomeWOLCanBeCreatedWithCommunicator()
        {
            var mockCommunicator = new Mock<IWOLCommunicator>();

            var wol = new HomeWOL(mockCommunicator.Object);
            Assert.IsType<HomeWOL>(wol);
        }

        [Fact]
        public async void HomeWOLCanSendExpectedMagicPacketToIP()
        {

            var macBytes = new byte[] { 0x12, 0x23, 0x34, 0x45, 0x56, 0x67 };
            var mac = new PhysicalAddress(macBytes);
            var ip = new IPAddress(new byte[] { 192, 168, 1, 255 });

            var expectedPayload = new List<byte> { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            for(int i = 0; i < 16; i++)
            {
                expectedPayload.AddRange(macBytes);
            }
            var payloadBytes = expectedPayload.ToArray();

            var mockCommunicator = new Mock<IWOLCommunicator>();
            mockCommunicator.Setup(x => x.SendAsync(payloadBytes, ip))
                .ReturnsAsync(0);

            var wol = new HomeWOL(mockCommunicator.Object);

            await wol.Wake(mac, ip);

            mockCommunicator.VerifyAll();

        }

        [Fact]
        public async void HomeWOLCanSendExpectedMagicPacketToIPWithStringArgs()
        {

            var macBytes = new byte[] { 0x12, 0x23, 0x34, 0x45, 0x56, 0x67 };
            var mac = new PhysicalAddress(macBytes);
            var ip = new IPAddress(new byte[] { 192, 168, 1, 255 });

            var expectedPayload = new List<byte> { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            for (int i = 0; i < 16; i++)
            {
                expectedPayload.AddRange(macBytes);
            }
            var payloadBytes = expectedPayload.ToArray();

            var mockCommunicator = new Mock<IWOLCommunicator>();
            mockCommunicator.Setup(x => x.SendAsync(payloadBytes, ip))
                .ReturnsAsync(0);

            var wol = new HomeWOL(mockCommunicator.Object);

            await wol.Wake("12:23:34:45:56:67", "192.168.1.255");

            mockCommunicator.VerifyAll();

        }

        [Fact]
        public void MacStringToPhysicalAddressConvertsString()
        {

            PhysicalAddress expected = new PhysicalAddress(new byte[] { 0x12, 0x23, 0x34, 0x45, 0x56, 0x67 });
            var actual = HomeWOL.MacStringToPhysicalAdress("12:23:34:45:56:67");

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void IpStringToIPAddressConvertsString()
        {

            IPAddress expected = new IPAddress(new byte[] { 192, 168, 1, 1 });
            var actual = HomeWOL.IPStringToIPAddress("192.168.1.1");

            Assert.Equal(expected, actual);

        }

    }
}
