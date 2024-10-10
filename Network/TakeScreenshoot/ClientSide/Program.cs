using System.Drawing;
using System.Net;
using System.Net.Http.Headers;
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
        var stream = client.GetStream();
        while (true)
        { 
            var bytes = takeScreen();
            //Byte in uzunlugunu streame gonder
            byte[] lengthBytes = BitConverter.GetBytes(bytes.Length);
            stream.Write(lengthBytes, 0, lengthBytes.Length);

            //Bytleri streame gonder
            stream.Write(bytes, 0, bytes.Length);
            Thread.Sleep(5000);
        }
      

    }
}
catch (Exception ex)
{

    Console.WriteLine(ex.Message);
}
byte[] takeScreen()
{
    Bitmap memoryImage;
    memoryImage = new Bitmap(1920, 1080);
    Size s = new Size(memoryImage.Width, memoryImage.Height);

    Graphics memoryGraphics = Graphics.FromImage(memoryImage);

    memoryGraphics.CopyFromScreen(0, 0, 0, 0, s);
    using (MemoryStream ms = new MemoryStream())
    {
        memoryImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        return ms.ToArray();
    }
}