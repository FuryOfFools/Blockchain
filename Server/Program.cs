using System.Net;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(IPAddress.Parse("127.0.0.1"), 8005);
            server.Listen();
        }
    }
}
