using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Adventofcode_day1.Html;
using Adventofcode_day1.Log;
using Adventofcode_day1.Settings;

namespace Adventofcode_day1.Server
{
    public class Routes
    {
        public static  Dictionary<string, string> CachedRoutes = new Dictionary<string, string>();
        
        public static List<string> GetRegisterRoutes()
        {
            return CachedRoutes.ToList().Select(x => x.Key).ToList();
        }
        
        public static void RegisterRoute(string route, HtmlBuilder view)
        {
            string htmlConetent = view.GetHTML();
            try
            {
                Logger.LogMessage("Register Route -> " + route);  
            }
            catch (Exception e)
            {
                Console.WriteLine("Parse View error");
                Log.Logger.LogMessage("Failed Register Route -> " + route);
            }
            CachedRoutes.Add(route, htmlConetent);
        }
        
        public static string GetRoute(string route)
        {
            Instance.debugger.DebugInfo("Get Route -> " + route, "ROUTES");
            if (CachedRoutes.ContainsKey("/" + route))
            {
                return CachedRoutes["/" + route];
            }
            return CachedRoutes["/error"];
        }
    }
}