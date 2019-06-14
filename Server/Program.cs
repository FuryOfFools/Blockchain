using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static int port = 8005;

        struct clients
        {
            int ip;
            string port;

        }

        static void Main(string[] args)
        {
            //получение адреса для запуска сокета
            var ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

            //создание сокета
            using (var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                try
                {
                    //связываем сокет с локальной точкой в которой будем принимать данные
                    listenSocket.Bind(ipPoint);

                    //начало прослушивания
                    listenSocket.Listen(10);

                    Console.WriteLine("Сервер запущен. Ожидание подключений...");

                    while (true)
                    {
                        using (Socket handler = listenSocket.Accept())
                        {
                            //получаем сообщение
                            var builder = new StringBuilder();
                            int bytes = 0;//количество полученных байтов
                            byte[] data = new byte[256];//буфер дляполучаемых данных

                            do
                            {
                                bytes = handler.Receive(data);
                                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                            }
                            while (handler.Available > 0);

                            Console.WriteLine($"{DateTime.Now.ToShortTimeString()} : {builder.ToString()} " +
                                $"ip {handler.RemoteEndPoint} connected {handler.Connected}");

                            //отправка ответа
                            string message = "Сообщение доставлено";
                            data = Encoding.Unicode.GetBytes(message);
                            handler.Send(data);
                            // закрываем сокет
                            handler.Shutdown(SocketShutdown.Both);
                            //handler.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
        }
    }
}
