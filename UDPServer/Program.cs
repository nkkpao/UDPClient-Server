using System.Net;
using System.Net.Sockets;
using System.Text;

using Socket udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

var localIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);

// привязываем сокет к локальной точке 127.0.0.1:5555
// и начинаем прослушивание входящих сообщений
udpSocket.Bind(localIP);

Console.WriteLine("UDP-сервер запущен...");

byte[] data = new byte[256];

EndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0);

while (true)
{

    var result = await udpSocket.ReceiveFromAsync(data, remoteIp);
    var message = Encoding.UTF8.GetString(data, 0, result.ReceivedBytes);

    Console.WriteLine($"Получено {result.ReceivedBytes} байт");
    Console.WriteLine($"Удаленный адрес: {result.RemoteEndPoint}");
    Console.WriteLine(message);

    byte[] backData = Encoding.UTF8.GetBytes(message);

    await udpSocket.SendToAsync(backData, result.RemoteEndPoint);
}