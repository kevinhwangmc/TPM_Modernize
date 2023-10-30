using HPNSDataGenerator;
using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

class Server
{
    TcpListener server = null;
    public Server(string ip, int port)
    {
        IPAddress localAddr = IPAddress.Parse(ip);
        server = new TcpListener(localAddr, port);
        server.Start();
        StartListener();
    }

    public void StartListener()
    {
        try
        {
            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");

                Thread t = new Thread(new ParameterizedThreadStart(HandleDeivce));
                t.Start(client);
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
            server.Stop();
        }
    }


    public void HandleDeivce(Object obj)
    {
        TcpClient client = (TcpClient)obj;
        var stream = client.GetStream();

        Random rnd = new Random();
        
        try
        {
            string binMode = ConfigurationManager.AppSettings["BINMODE"];
            while (true)
            {
                var data = new DataPacketWrapper(Convert.ToInt32(binMode));
                var bytes = data.GetBytes();
                stream.Write(bytes, 0, bytes.Length);
                data.Dispose();
                Thread.Sleep(rnd.Next(2000, 10000));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: {0}", e.ToString());
            client.Close();
        }
    }

    public void HandleDeivce1(Object obj)
    {
        TcpClient client = (TcpClient)obj;
        var stream = client.GetStream();
        string imei = String.Empty;
        int i = 0;
        try
        {
            string folderPath = @"SampleData\Gary\";

            foreach (string file in Directory.EnumerateFiles(folderPath, "*.txt"))
            {
                if (i == 0)
                {
                    List<string> lstStr = new();
                    IEnumerable<string> lines = File.ReadLines(file);
                    foreach (var str in lines)
                    {
                        //Gary
                        //if (str.Trim() != "" && str.Length > 62)
                        //    lstStr.Add(String.Concat(str.Substring(0, 62).Where(c => !Char.IsWhiteSpace(c))));
                        //else
                        //    lstStr.Add(str);

                        //Rixin
                        if (str.Trim() != "" && str.Length > 47)
                            lstStr.Add(String.Concat(str.Substring(0, 47).Where(c => !Char.IsWhiteSpace(c))));
                        else
                            lstStr.Add(str);
                    }

                    var binaryData = StringToByteArray(String.Join("", lstStr));
                    stream.Write(binaryData, 0, binaryData.Length);
                }
                i++;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: {0}", e.ToString());
            client.Close();
        }
    }

    public static byte[] StringToByteArray(string hex)
    {
        return Enumerable.Range(0, hex.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                         .ToArray();
    }
}
