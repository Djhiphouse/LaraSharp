using System;
using System.Net;
using System.Threading.Tasks;
using Adventofcode_day1.Html;
using Adventofcode_day1.Javascript;
using Adventofcode_day1.Log;
using Adventofcode_day1.Models;
using Adventofcode_day1.Server;
using Adventofcode_day1.Views;

namespace Adventofcode_day1.Settings
{
    public class Instance
    {
        public static HttpServer actionHandler;
        public static Settings settings = new Settings();
        public static DomEditor domdocument;
        public static Debugger debugger;
        public static ApiRouter api;
        public static Http http;
        public static string Host {get; set;}
        public static string Port {get; set;}
        
        public static void Initialize()
        {
            debugger = new Debugger();
            settings = new Settings();
            
            settings.Initializ();
            settings.SetSettings();
            
            Host = settings.GetSetting("HOST");
            Port = settings.GetSetting("PORT");
            
            actionHandler = new HttpServer();
            api = new ApiRouter();
            domdocument = new DomEditor();
        }
        
        public static void SqlInitialize()
        {
            User.Initialize(settings);
            Logs.Initialize(settings);
            session.Initialize(settings);
        }
        
        public static void ViewsInitialize()
        {
            Login.Initialize();
            LogsView.Initialize();
        }
        
        public static void RegisterRoute(string route, HtmlBuilder view)
        {
            Routes.RegisterRoute(route, view);
        }
        
        public static void RegisterApiRoute(string route, Func<HttpListenerRequest, string> action)
        {
            api.AddApi(route, request => Task.FromResult(action(request)));
        }
        
        public static void Migrate()
        {
            User.Migrate();
            session.Migrate();
        }

        public static void StartUp()
        {
            http = new Http();
            Task.Run(() => actionHandler.StartServer());
            Task.Run(() => new DomEditor().EditDomListener());
            Task.Run(() => api.StartServerAsync());
            http.Start();
        }

        public static void ManagedUserInput(string[] args)
        {
            if (args.Length > 0 && args[0] == "migrate")
            {
                Migrate();
                Environment.Exit(0);
            }else if (args.Length > 0 && args[0].StartsWith("model"))
            {
                Utils.GenerateModelClass(String.Join(" ", args).Replace("model:", ""));
            }
        }
    }
    
    
}