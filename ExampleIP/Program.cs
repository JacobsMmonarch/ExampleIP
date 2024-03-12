using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


class Client
{
    static void Main()
    {
        TcpClient tcpClient = new TcpClient();

        try
        {
            Console.WriteLine("Подключение к серверу...");
            tcpClient.Connect("localhost", 7191);

            Console.WriteLine($"Подключение к {((IPEndPoint)tcpClient.Client.LocalEndPoint).ToString()} установлено.");

            Console.Write("Введите слово: ");
            string message = Console.ReadLine();

            SendMessage(tcpClient, message);

            string response = ReceiveMessage(tcpClient);
            Console.WriteLine($"Ответ от сервера: {response}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        finally
        {
            tcpClient.Close();
        }
    }

    static void SendMessage(TcpClient client, string message)
    {
        NetworkStream stream = client.GetStream();
        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);
    }

    static string ReceiveMessage(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] data = new byte[256];
        int bytesRead = stream.Read(data, 0, data.Length);
        return Encoding.UTF8.GetString(data, 0, bytesRead);
    }
}
