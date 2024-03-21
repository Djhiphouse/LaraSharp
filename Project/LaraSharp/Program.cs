using System;
using System.Runtime.InteropServices;
using System.Threading;
using Adventofcode_day1.Server;
using Adventofcode_day1.Settings;

class Program
{
    static void Main(string[] args)
    {
        Settings settings = new Settings();
        settings.Initializ();
        settings.SetSettings();
        Http http = new Http();
        http.Start();
        Thread.Sleep(-1);
    }
    
    
}

