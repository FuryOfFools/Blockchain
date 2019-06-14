using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Server
{
    class Program
    {
        static int port = 8005;

        [Serializable]
        struct Clients
        {
            //todo: добавить имя пользователя для наглядности
            public string ip;
            public int port;
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
                        var clients = new List<Clients> { };
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

                            Console.WriteLine($"{DateTime.Now.ToShortTimeString()} : {builder.ToString()}");

                            #region СтруктураПользователя

                            string clientIP;
                            int clientPort;
                            string endp = Convert.ToString(handler.RemoteEndPoint);
                            
                            string[] words = endp.Split(new char[] { ':' });
                            clientIP = words[0];
                            clientPort = Convert.ToInt32(words[1]);
                            Clients client;
                            client.ip = clientIP;
                            client.port = clientPort;
                            //client.connected = handler.Connected;
                            clients.Add(client);
                            #endregion


                           
                            //todo: не рабоает доделать 
                            #region ИнтерфейсВзаимодействия
                            //1 получить список активных пользователей
                            //2 удалить себя из списка и disconnect
                            while (handler.Connected)
                                switch (Convert.ToInt32(builder.ToString()))
                                {
                                    case 1:
                                        string message = "Активные пользователи";
                                        data = Encoding.Unicode.GetBytes(message);
                                        handler.Send(data);
                                        //todo: метод вывода активных пользователей + интерфейс взаимодейсвтия
                                        Console.WriteLine("Не готовый метод!");
                                        break;
                                    case 2:
                                        message = "Пользователь удален";
                                        data = Encoding.Unicode.GetBytes(message);
                                        handler.Send(data);

                                        clients.Remove(client);
                                        //закрываем сокет
                                        handler.Shutdown(SocketShutdown.Both);
                                        break;
                                    default:
                                        clients.Remove(client);
                                        //закрываем сокет
                                        handler.Shutdown(SocketShutdown.Both);
                                        break;
                                }
                            #endregion
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
