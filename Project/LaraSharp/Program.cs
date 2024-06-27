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
        Instance.RegisterRoute("/welcome", Login.htmlBuilder);
        Instance.RegisterRoute("/logs", LogsView.htmlBuilder);
         
        Instance.RegisterApiRoute("/api/usertable",  request =>
        {
            var users =  Task.Run(() => User.GetUsersForTable());
            return JsonConvert.SerializeObject(users);
        });
        
        Instance.RegisterApiRoute("/api/logs",  request =>
        {
            var users =  Task.Run(() => Logs.GetLogsForTable());
            return JsonConvert.SerializeObject(users);
        });
        
        Instance.StartUp();
        Thread.Sleep(-1);
    }
}

