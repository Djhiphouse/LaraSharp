using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Adventofcode_day1.Server
{
    public class Routes
    {
        public static  Dictionary<string, string> CachedRoutes = new Dictionary<string, string>();
        
        public static List<string> GetRegisterRoutes()
        {
            return CachedRoutes.ToList().Select(x => x.Key).ToList();
        }
        
        public static void RegisterRoute(string route, string view)
        {
            string viewPath = "Views/" + view + ".html";
            string htmlConetent = String.Empty;
            try
            {
              htmlConetent = File.ReadAllText(viewPath);
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
            Console.WriteLine("Route: " + route);
            if (CachedRoutes.ContainsKey("/" + route))
            {
                return CachedRoutes["/" + route];
            }
            return CachedRoutes["/error"];
        }
    }
}