// See https://aka.ms/new-console-template for more information
using System.Net.Sockets;
using System.Text;
using TCP_Client;
using static Google.Rpc.Context.AttributeContext.Types;

new Thread(() =>
{
    Thread.CurrentThread.IsBackground = true;
    //Connect("172.20.10.2", "Hello I'm Device 1...");
    //Connect("192.168.1.73", "Hello I'm Device 1...");
    Connect("192.168.1.6", "Hello I'm Device 1...");
}).Start();

//new Thread(() =>
//{
//    Thread.CurrentThread.IsBackground = true;
//    Connect("192.168.0.10", "Hello I'm Device 2...");
//}).Start();


Console.ReadLine();

static async void Connect(String server, String message)
{
    try
    {
        Int32 port = 13000;
        TcpClient client = new TcpClient(server, port);

        NetworkStream stream = client.GetStream();

        int count = 0;
        while (count++ < 10)
        {
            // Translate the Message into ASCII.
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            // Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length);
            //Console.WriteLine("Sent: {0}", message);

            // Bytes Array to receive Server Response.
            data = new Byte[15073];
            //String response = String.Empty;

            // Read the Tcp Server Response Bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);

            PubSubCls pubSub = new();
            //await pubSub.PublishMessageWithCustomAttributesAsync("g-npe-prj-rhmod-dev", "test-tpm", ByteArrayToString(data));
            await pubSub.PublishMessageWithCustomAttributesAsync("g-npe-prj-rhmod-dev", "test-tpm", data);

            //PublishMessagesAsyncSample pubsub2 = new();
            //await pubsub2.PublishMessagesAsync("g-npe-prj-rhmod-dev", "test-tpm", data);

            //response = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            //Console.WriteLine(System.Text.Encoding.ASCII.GetString(data, 0, bytes));
            //Console.WriteLine("Received: {0}", ByteArrayToString(data));
            Console.WriteLine("Received: {0}", ByteArrayToString(data));
            //Console.WriteLine(string.Concat(data.Select(b => Convert.ToString(b, 2))));

            //Feed byte array into PUB/SUB

            Thread.Sleep(2000);
        }

        stream.Close();
        client.Close();
    }
    catch (Exception e)
    {
        Console.WriteLine("Exception: {0}", e);
    }

    Console.Read();
}

static string ByteArrayToString(byte[] ba)
{
    StringBuilder hex = new StringBuilder(ba.Length * 2);
    foreach (byte b in ba)
        hex.AppendFormat("{0:x2}", b);
    return hex.ToString();
}