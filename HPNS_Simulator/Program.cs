﻿using HPNSDataGenerator;
using HPNSDataGenerator.util;

AnalysisTblV3 data = new AnalysisTblV3();
var dd = data.GetBytes();

Thread t = new Thread(delegate ()
{
    // replace the IP with your system IP Address...
    //Server myserver = new Server("172.20.10.2", 13000);
    //Server myserver = new Server("192.168.1.73", 13000);
    Server myserver = new Server("192.168.1.6", 13000);
});
t.Start();

Console.WriteLine("Server Started...!");
Console.ReadLine();