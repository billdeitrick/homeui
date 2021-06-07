using HomeWol;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using Xunit;

namespace HomeWOLTests
{
    public class HomeWolTests
    {

        [Fact]
        public void HomeWolCanBeCreatedWithCommunicator()
        {
            var mockCommunicator = new Mock<IWolCommunicator>();

            var wol = new HomeWol.HomeWol(mockCommunicator.Object);
            Assert.IsType<HomeWol.HomeWol>(wol);
        }

        [Fact]
        public async void HomeWolCanSendExpectedMagicPacketToIp()
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

            var mockCommunicator = new Mock<IWolCommunicator>();
            mockCommunicator.Setup(x => x.SendAsync(payloadBytes, ip))
                .ReturnsAsync(0);

            var wol = new HomeWol.HomeWol(mockCommunicator.Object);

            await wol.Wake(mac, ip);

            mockCommunicator.VerifyAll();

        }

        [Fact]
        public async void HomeWolCanSendExpectedMagicPacketToIpWithStringArgs()
        {

            var macBytes = new byte[] { 0x12, 0x23, 0x34, 0x45, 0x56, 0x67 };
            var ip = new IPAddress(new byte[] { 192, 168, 1, 255 });

            var expectedPayload = new List<byte> { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            for (int i = 0; i < 16; i++)
            {
                expectedPayload.AddRange(macBytes);
            }
            var payloadBytes = expectedPayload.ToArray();

            var mockCommunicator = new Mock<IWolCommunicator>();
            mockCommunicator.Setup(x => x.SendAsync(payloadBytes, ip))
                .ReturnsAsync(0);

            var wol = new HomeWol.HomeWol(mockCommunicator.Object);

            await wol.Wake("12:23:34:45:56:67", "192.168.1.255");

            mockCommunicator.VerifyAll();

        }

        [Fact]
        public void MacStringToPhysicalAddressConvertsString()
        {

            PhysicalAddress expected = new PhysicalAddress(new byte[] { 0x12, 0x23, 0x34, 0x45, 0x56, 0x67 });
            var actual = HomeWol.HomeWol.MacStringToPhysicalAdress("12:23:34:45:56:67");

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void IpStringToIpAddressConvertsString()
        {

            IPAddress expected = new IPAddress(new byte[] { 192, 168, 1, 1 });
            var actual = HomeWol.HomeWol.IpStringToIpAddress("192.168.1.1");

            Assert.Equal(expected, actual);

        }

    }
}
