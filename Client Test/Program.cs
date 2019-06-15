using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client_Test
{
    class Program
    {
        static int serverPort = 8005;
        static string serverAddress = "127.0.0.1";
        static void Main(string[] args)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(serverAddress), serverPort);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);
                Console.WriteLine("Введите ник:");
                string name = Console.ReadLine();
                Console.WriteLine($"1 - Вывести список пользователей:\n2 - Удалить пользователя");
                Console.WriteLine("Введите сообщение:");
                string message = name + "|" + Console.ReadLine();
                byte[] data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);

                // получаем ответ
                data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт

                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                Console.WriteLine(builder.ToString());
                // закрываем сокет
                //todo: удаление себя из списка в сервере
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
