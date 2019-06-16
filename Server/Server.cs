using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace Server
{
    struct Client
    {
        public string name;//todo: переделать на чисто айпи
        public string ip;
        public int port;
    }
    class Server
    {
        /// <summary>
        /// ip адрес сервера
        /// </summary>
        public IPAddress IP { get; private set; }
        /// <summary>
        /// порт сервера
        /// </summary>
        public int port { get; private set; }
        /// <summary>
        /// список клиентов в сети
        /// </summary>
        public List<Client> clients { get; private set; }

        public Server(IPAddress IP, int port)
        {
            this.IP = IP;
            this.port = port;
        }
        #region ClientsActions
        /// <summary>
        /// Добавить клиента в список
        /// </summary>
        /// <param name="clients">Список клиентов</param>
        /// <param name="name">Ник клиента</param>
        /// <param name="endPoint">адрес клиента</param>
        /// <returns>Список клиентов</returns>
        private List<Client> AddClient(List<Client> clients, string name, EndPoint endPoint)
        {
            string endp = Convert.ToString(endPoint);
            string[] IPwords = endp.Split(new char[] { ':' });

            Client cl;
            if (!clients.Exists(x => x.name == name))
            {
                cl.name = name;
                cl.ip = IPwords[0];
                cl.port = Convert.ToInt32(IPwords[1]);
                clients.Add(cl);
            }
            else
            {
                for(int i = 0; i < clients.Count; i++)
                {
                    if(clients[i].name == name)
                    {
                        RemoveClient(name, clients);
                        AddClient(clients, name, endPoint);
                    }
                }
            }
            return clients;
        }
        /// <summary>
        /// Вывести список всех клиентов
        /// </summary>
        /// <param name="clients">Список клиентов</param>
        /// <returns>Строка со всеми клиентами</returns>
        private string PrintClients(List<Client> clients)
        {
            string message = "";
            foreach (Client cl in clients)
            {
                message += $"{cl.name} {cl.ip} {cl.port}{Environment.NewLine}";
            }
            return message;
        }
        /// <summary>
        /// Удалить клиента
        /// </summary>
        /// <param name="name">Ник клиента</param>
        /// <param name="clients">Список клиентов</param>
        private void RemoveClient(string name, List<Client> clients)
        {
            foreach (Client cl in clients)
            {
                if (cl.name == name)
                {
                    clients.Remove(cl);
                    return;
                }
            }
        }
        #endregion

        #region SendRecieve
        /// <summary>
        /// Принять сообщение
        /// </summary>
        /// <param name="handler">Сокет клиента</param>
        /// <returns>Принятое сообщение в виде строки</returns>
        private string RecieveMessage(Socket handler)
        {
            var builder = new StringBuilder();
            int bytes = 0;//количество полученных байтов
            byte[] data = new byte[256];//буфер дляполучаемых данных
            do
            {
                bytes = handler.Receive(data);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (handler.Available > 0);
            return builder.ToString();
        }
        /// <summary>
        /// Отправить сообщение 
        /// </summary>
        /// <param name="handler">Сокет клинта</param>
        /// <param name="message">Сообщение в виде строки</param>
        private void SendMessage(Socket handler, string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            handler.Send(data);
        }
        #endregion

        /// <summary>
        /// Функция прослушивания
        /// </summary>
        public void Listen()
        {
            var ipPoint = new IPEndPoint(IP, port);
            //создание сокета
            using (var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                listenSocket.Bind(ipPoint);//связываем сокет с локальной точкой
                listenSocket.Listen(10);//начало прослушивания
                clients = new List<Client> { };
                while (true)
                {
                    using (Socket handler = listenSocket.Accept())
                    {
                        #region Сообщение
                        //получаем сообщение
                        string[] Words = RecieveMessage(handler).Split(new char[] { '|' });
                        string message = Words[1];
                        string name = Words[0];
                        Console.WriteLine($"{DateTime.Now.ToShortTimeString()} {name}: {message}");

                        clients = AddClient(clients, name, handler.RemoteEndPoint);
                        #endregion

                        #region ИнтерфейсВзаимодействия
                        switch (Convert.ToInt32(message))
                        {
                            case 1:
                                //получить список активных пользователей
                                message = $"Активные пользователи:{Environment.NewLine}{PrintClients(clients)}"; ;
                                SendMessage(handler, message);
                                handler.Shutdown(SocketShutdown.Both);
                                break;
                            case 2:
                                RemoveClient(name, clients);
                                handler.Shutdown(SocketShutdown.Both);
                                break;
                            default:
                                handler.Shutdown(SocketShutdown.Both);
                                break;
                        }
                        #endregion
                    }
                }
            }
        }
    }
}
