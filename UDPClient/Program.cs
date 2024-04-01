using System.Net.Sockets;
using System.Text;

using var udpClient = new UdpClient();
udpClient.Connect("127.0.0.1", 5555);
Console.WriteLine("UDP-клиент запущен...");

for (int i = 1; ; i++)
{
    // отправляемые данные
    string message = i.ToString();
    // преобразуем в массив байтов
    byte[] data = Encoding.UTF8.GetBytes(message);

    // отправляем данные
    int bytes = await udpClient.SendAsync(data);
    Console.WriteLine($"Отправлено {bytes} байт");

    // получаем данные
    var result = await udpClient.ReceiveAsync();
    // предположим, что отправлена строка, преобразуем байты в строку
    var resMessage = Encoding.UTF8.GetString(result.Buffer);

    Console.WriteLine($"Получено {result.Buffer.Length} байт");
    Console.WriteLine($"Удаленный адрес: {result.RemoteEndPoint}");
    Console.WriteLine(resMessage);

    Task.Delay(i * 100).Wait();
}

