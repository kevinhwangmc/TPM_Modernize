
using HPNSDataGenerator;
using HPNSDataGenerator.util;
using System.Net.Sockets;
using System.Configuration;

new Thread(() =>
{
    try
    {
        string serverIPAddr = ConfigurationManager.AppSettings["ServerIPAddr"];
        Thread.CurrentThread.IsBackground = true;
        Connect(serverIPAddr, "Hello I'm Device 1...");
    }
    catch (Exception e)
    {
        Console.WriteLine("Exception: {0}", e);
    }
    
}).Start();

static async void Connect(String server, String message)
{
    try
    {
        Console.WriteLine("HPNS Started...!");

        string binMode = ConfigurationManager.AppSettings["BINMODE"];
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
                var data = new DataPacketWrapper(Convert.ToInt32(binMode));
                var bytes = data.GetBytes();
                stream.Write(bytes, 0, bytes.Length);
                data.Dispose();
                Thread.Sleep(rnd.Next(1000, 2000));

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

Console.ReadLine();