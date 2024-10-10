
using System.Net;
using System.Net.Sockets;

var port = 27001;
var IpAd = IPAddress.Parse("192.168.100.8");
var endPoint = new IPEndPoint(IpAd, port);

var listener = new TcpListener(endPoint);

try
{
    listener.Start();
    while (true)
    {
        _ = Task.Run(() => {
            while (true)
            {
                var client = listener.AcceptTcpClient();
                Console.WriteLine($"{client.Client.RemoteEndPoint} connected");

                var stream = client.GetStream();

                var fileSizeByte = new byte[4];

                int readBytes = stream.Read(fileSizeByte,0,fileSizeByte.Length);
               

                int fileSize = BitConverter.ToInt32(fileSizeByte,0);

                byte[] fileBytes = new byte[fileSize];

                int len2 = 0;
                int totalBytes = 0;
                do
                {
                    len2 = stream.Read(fileBytes, totalBytes,fileSize - totalBytes);
                    if(len2 ==0)
                    {
                        break;
                    }
                    totalBytes += len2;
                }

                while (len2 > 0 && fileSize>totalBytes);
                var desPath = $@"C:\Users\amiro\Desktop\New folder\{Guid.NewGuid()}.png";
                using (var writeFs = new FileStream(desPath, FileMode.Create, FileAccess.Write))
                {
                    writeFs.Write(fileBytes, 0, totalBytes);
                }
                
            }
        });
    }


}
catch (Exception ex)
{

    Console.WriteLine(ex.Message); ;
}