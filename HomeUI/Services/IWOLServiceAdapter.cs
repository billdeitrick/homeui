namespace HomeUI.Services
{
    public interface IWOLServiceAdapter
    {
        void Wake(string mac, string ip);

    }
}