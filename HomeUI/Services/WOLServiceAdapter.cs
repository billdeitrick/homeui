using WakeOnLanSharp;

namespace HomeUI.Services
{
    public class WOLServiceAdapter : IWOLServiceAdapter
    {

        private WakeOnLan _wol;

        public WOLServiceAdapter()
        {
            _wol = new WakeOnLan(9);
        }

        public void Wake(string mac, string ip)
        {
            _wol.SendMagicPacket(WakeOnLan.ParseMacAddress(mac), ip);
        }

    }
}
