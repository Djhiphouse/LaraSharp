using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Adventofcode_day1.Html;
using Adventofcode_day1.Log;
using Adventofcode_day1.Settings;

namespace Adventofcode_day1.Server
{
    public class Routes
    {
        public static  Dictionary<string, string> CachedRoutes = new Dictionary<string, string>();
        public static List<string> CachedRoutesRedirects = new List<string>();
        
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
        
        public async Task AddCachedRoute(string route)
        {
            if (!CachedRoutesRedirects.Any() || !CachedRoutesRedirects.Last().Contains(route))
            {
                CachedRoutesRedirects.Add("/" + route);
            }
    
            await Task.CompletedTask;
        }

        public void Redirect(string route)
        {
            Instance.domdocument.AddJavascript($@"window.location.href = 'http://{Instance.Host}:{Instance.Port}{route}'");
            CachedRoutesRedirects.Add(route);
            Instance.debugger.DebugInfo("Redirect -> " + route, "ROUTES");
        }

        public void RedirectBack()
        {
            if (CachedRoutesRedirects.Count <= 1)
            {
                Instance.debugger.DebugError("No route to redirect back", "ROUTES ERROR");
                return;
            }
            
            string secondToLastRoute = CachedRoutesRedirects[CachedRoutesRedirects.Count - 2];
            
            Instance.domdocument.AddJavascript($@"window.location.href = 'http://{Instance.Host}:{Instance.Port}{secondToLastRoute}'");
            
            CachedRoutesRedirects.RemoveAt(CachedRoutesRedirects.Count - 2);
            
            Instance.debugger.DebugInfo("Redirect -> " + secondToLastRoute, "ROUTES");
        }


    }
}