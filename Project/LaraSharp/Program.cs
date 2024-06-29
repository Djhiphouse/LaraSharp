using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Adventofcode_day1.Javascript;
using Adventofcode_day1.Models;
using Adventofcode_day1.Server;
using Adventofcode_day1.Settings;
using Adventofcode_day1.Views;
using Newtonsoft.Json;


public class Program
{
    static async Task Main(string[] args)
    {
        
        Instance.ManagedUserInput(args);
        Instance.Initialize();
        Instance.SqlInitialize();
        Instance.ViewsInitialize();
        
        Instance.RegisterRoute("/welcome", WelcomeView.htmlBuilder);
        Instance.RegisterRoute("/logs", LogsView.htmlBuilder);
        Instance.RegisterRoute("/error", ErrorView.htmlBuilder);
        Instance.RegisterRoute("/tutorial", TutorialView.htmlBuilder);
        
        Instance.StartUp();
        Thread.Sleep(-1);
    }
}

