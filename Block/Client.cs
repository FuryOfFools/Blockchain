using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace OnlineInteractions
{
    struct ClientStruct
    {
        public string name;//todo: переделать на чисто айпи
        public string ip;
        public int port;
    }

    class Client
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
        /// Ник пользователя
        /// </summary>
        public string name { get; private set; }//todo: убрать потом
        /// <summary>
        /// список клиентов в сети
        /// </summary>
        public List<ClientStruct> clients { get; private set; }

        public Client(IPAddress IP, int port, string name)
        {
            this.name = name;//todo: убрать потом
            this.IP = IP;
            this.port = port;
        }
        public Socket StartConnect(int num)
        {
            IPEndPoint ipPoint = new IPEndPoint(IP, port);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // подключаемся к удаленному хосту
            socket.Connect(ipPoint);

            string message = $"{name}|{num}";//отправляем запрос на получение списка пользователей сети
            byte[] data = Encoding.Unicode.GetBytes(message);
            socket.Send(data);

            return socket;
        }
        public List<ClientStruct> Connect()
        {
            Socket socket = StartConnect(1);
            // получаем ответ
            byte[] data = new byte[256]; // буфер для ответа
            StringBuilder builder = new StringBuilder();
            int bytes = 0; // количество полученных байт

            do
            {
                bytes = socket.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);
            clients = ClientsList(builder.ToString());
            //todo: передавать список клиентов//Console.WriteLine(builder.ToString());

            // закрываем сокет
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            return clients;

        }
        public void Disconect()
        {
            Socket socket = StartConnect(2);
            // закрываем сокет
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        private List<ClientStruct> ClientsList(string clientsString)
        {
            string[] strClients = clientsString.Split(new char[] { '\n' });
            foreach(string sCl in strClients)
            {
                string[] strClient = sCl.Split(new char[] { ' ' });
                ClientStruct client = new ClientStruct();
                client.name = strClient[0];
                client.ip = strClient[1];
                client.port = Convert.ToInt32(strClient[2]);
                clients.Add(client);
            }
            return clients;
        }
    }
}
