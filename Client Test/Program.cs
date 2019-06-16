using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client_Test
{
    struct ClientStruct
    {
        public string name;//todo: переделать на чисто айпи
        public string ip;
        public int port;
    }

    class Program
    {
        static int serverPort = 8005;
        static string serverAddress = "127.0.0.1";
        static string name = "defName";

        static void Main(string[] args)
        {
            Console.WriteLine("Введите ник");
            name = Console.ReadLine();
            Client client = new Client(IPAddress.Parse(serverAddress), serverPort, name);
            var clients = client.Connect();
            client.Disconect();
        }
    }
}
