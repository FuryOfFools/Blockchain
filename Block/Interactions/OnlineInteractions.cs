using System;
using System.Net;

namespace Interactions
{
    public class OnlineInteractions
    {
        public void ConnectToServer()
        {
            int serverPort = 8005;
            string serverAddress = "127.0.0.1";
            Console.WriteLine("Введите ник");
            string name = Console.ReadLine();
            Client client = new Client(IPAddress.Parse(serverAddress), serverPort, name);
            var clients = client.Connect();
        }

        public void Disconnect()//todo: добавить на выключение окна
        {
            int serverPort = 8005;
            string serverAddress = "127.0.0.1";
            Console.WriteLine("Введите ник");
            string name = Console.ReadLine();//todo: исправить костыль

            Client client = new Client(IPAddress.Parse(serverAddress), serverPort, name);
            client.Disconect();
        }
    }
}
