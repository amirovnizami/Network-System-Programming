using System.Data;
using System.Net;
using System.Net.Sockets;

var port = 27001;

var ipAd = IPAddress.Parse("192.168.100.8");

var endPoint = new IPEndPoint(ipAd, port);

var listener = new TcpListener(endPoint);

try
{
    listener.Start();

    while (true)
    {
        var client = listener.AcceptTcpClient();

        _ = Task.Run(() =>
        {
            Console.WriteLine($"{client.Client.RemoteEndPoint} connected");
            var stream = client.GetStream();
            var sr = new StreamReader(stream);
            while (true)
            {
                var message = sr.ReadLine();
                Console.WriteLine($"{client.Client.RemoteEndPoint} : {message}");
            }
        });
    }
}
catch (Exception ex)
{

    Console.WriteLine(ex.Message);
}