using System.Net;
using System.Net.Sockets;

var port = 27001;

var IpAd = IPAddress.Parse("192.168.100.8");

var endPoint = new IPEndPoint(IpAd, port);

var client = new TcpClient();

try
{
	client.Connect(endPoint);
	if (client.Connected)
	{
		var sw = new StreamWriter(client.GetStream())
		{
			AutoFlush = true
		};
		while (true)
		{
			var path = @"C:\Users\amiro\Desktop\ConnectionString.txt";
			var despath = @"C:\Users\amiro\Desktop\New.txt";
            using (var fRead = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using(var fWrite = new FileStream(despath, FileMode.OpenOrCreate, FileAccess.Write))
				{
                    var len = 1024;
                    var bytes = new byte[len];
                    do
                    {
                        len = fRead.Read(bytes, 0, len);
						fWrite.Write(bytes, 0, len);
                    }
                    while (len > 0);
                }
				

            }
        }
	}
}
catch (Exception ex)
{

	Console.WriteLine(ex.Message); ;
}