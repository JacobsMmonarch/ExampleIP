using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Client
{
    static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Введите сообщение для отправки на сервер:");
            string message = Console.ReadLine();

            await SendMessageAsync(message);

            Console.WriteLine("Ожидание ответа от сервера...");
            string response = await ReceiveMessageAsync();
            Console.WriteLine($"Ответ от сервера: {response}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static async Task SendMessageAsync(string message)
    {
        using (HttpClient client = new HttpClient())
        {
            StringContent content = new StringContent(message, Encoding.UTF8, "text/plain");

            HttpResponseMessage response = await client.PostAsync("https://localhost:7191", content);
            response.EnsureSuccessStatusCode(); // Гарантирует успешный ответ от сервера
        }
    }

    static async Task<string> ReceiveMessageAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7191");
            response.EnsureSuccessStatusCode(); // Гарантирует успешный ответ от сервера

            return await response.Content.ReadAsStringAsync();
        }
    }
}

    //  Main(string[] args):
    //    Это точка входа в приложение.
    //    Запрашивает у пользователя ввод сообщения для отправки на сервер.
    //    Вызывает метод SendMessageAsync() для отправки сообщения на сервер.
    //    Выводит на консоль сообщение ожидания ответа.
    //    Вызывает метод ReceiveMessageAsync() для получения ответа от сервера.
    //    Выводит полученный ответ на консоль.

    //  SendMessageAsync(string message):
    //    Создает новый экземпляр HttpClient.
    //    Создает StringContent, содержащий переданное сообщение, указывая кодировку UTF-8 и тип контента "text/plain".
    //    Отправляет POST-запрос на сервер по указанному URL ("https://localhost:7191") с данными сообщения.
    //    EnsureSuccessStatusCode() гарантирует, что полученный ответ от сервера имеет успешный статус. Если статус не успешный, будет сгенерировано исключение.

    //  ReceiveMessageAsync():
    //    Создает новый экземпляр HttpClient.
    //    Отправляет GET-запрос на сервер по указанному URL ("https://localhost:7191").
    //    EnsureSuccessStatusCode() гарантирует, что полученный ответ от сервера имеет успешный статус. Если статус не успешный, будет сгенерировано исключение.
    //    Возвращает содержимое ответа в виде строки с помощью метода ReadAsStringAsync().















//using System;
//using System.Net;
//using System.Net.Http;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading.Tasks;


//class Client
//{
//    static void Main()
//    {
//        TcpClient tcpClient = new TcpClient();

//        try
//        {
//            Console.WriteLine("Подключение к серверу...");
//            tcpClient.Connect("localhost", 7191);

//            Console.WriteLine($"Подключение к {((IPEndPoint)tcpClient.Client.LocalEndPoint).ToString()} установлено.");

//            Console.Write("Введите слово: ");
//            string message = Console.ReadLine();

//            SendMessage(tcpClient, message);

//            string response = ReceiveMessage(tcpClient);
//            Console.WriteLine($"Ответ от сервера: {response}");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }
//        finally
//        {
//            tcpClient.Close();
//        }
//    }

//    static void SendMessage(TcpClient client, string message)
//    {
//        NetworkStream stream = client.GetStream();
//        byte[] data = Encoding.UTF8.GetBytes(message);
//        stream.Write(data, 0, data.Length);
//    }

//    static string ReceiveMessage(TcpClient client)
//    {
//        NetworkStream stream = client.GetStream();
//        byte[] data = new byte[256];
//        int bytesRead = stream.Read(data, 0, data.Length);
//        return Encoding.UTF8.GetString(data, 0, bytesRead);


//    }
//}
