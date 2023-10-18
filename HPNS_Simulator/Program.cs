
using HPNSDataGenerator;
using HPNSDataGenerator.util;
using System.Net.Sockets;

var ipaddr = ByteHelper.GetIPAddr();

new Thread(() =>
{
    Thread.CurrentThread.IsBackground = true;
    Connect("10.106.39.35", "Hello I'm Device 1...");
}).Start();

static async void Connect(String server, String message)
{
    try
    {
        Int32 port = 9005;
        TcpClient client = new TcpClient(server, port);
        NetworkStream stream = client.GetStream();
        Random rnd = new Random();
        int i = 0;
        try
        {
            while (true)
            {
                i++;
                //var randomBool = rnd.Next(0, 2) > 0;
                //var data = new DataPacketWrapper(randomBool);
                var data = new DataPacketWrapper(false);
                var bytes = data.GetBytes();
                stream.Write(bytes, 0, bytes.Length);
                data.Dispose();
                Thread.Sleep(rnd.Next(2000, 10000));


                Console.WriteLine("Pushed data packet. " + i.ToString());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: {0}", e.ToString());
            client.Close();
        }

    }
    catch (Exception e)
    {
        Console.WriteLine("Exception: {0}", e);
    }

    Console.Read();
}

//Thread t = new Thread(delegate ()
//{
//    // replace the IP with your system IP Address...
//    //Server myserver = new Server("172.20.10.2", 13000);
//    //Server myserver = new Server("192.168.1.73", 13000);
//    //Server myserver = new Server("192.168.1.6", 13000);
//    //Server myserver = new Server("192.168.219.112", 13000);
//    //vpn 10.24.15.210
//    Server myserver = new Server(ipaddr, 13000);

//});
//t.Start();

Console.WriteLine("HPNS Started...!");
Console.ReadLine();