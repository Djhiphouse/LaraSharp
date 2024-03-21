using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Adventofcode_day1.Server
{
    public class Http
    {
        public string host { get; set; }
        public int port { get; set; }
        
        private static HttpListener listener;
        public Http()
        {
            Settings.Settings settings = new Settings.Settings();
            this.host = "127.0.0.1";
            this.port = Int32.Parse(settings.GetSetting("PORT"));
            
            listener = new HttpListener();
            listener.Prefixes.Add($"http://{this.host}:{this.port}/");
            
            Console.WriteLine("Server is running on " + this.host + ":" + this.port);
            Routes.RegisterRoute("/welcome", "index");
            Routes.RegisterRoute("/error", "error");
            
            Log.Logger.LogMessage("Route Register -> Success");
        }
        
        public void Start()
        {
            listener.Start();
            Console.WriteLine("Listening for connections on " + $"{this.host}:{this.port}");
            Log.Logger.LogMessage("Http Status -> Started");
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            listener.Close();
        }

        private static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            while (runServer)
            {
                HttpListenerContext ctx = await listener.GetContextAsync();

                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;
                
                string view = String.Empty;
                string route = String.Empty;
                
                try
                {
                     route = req.Url.AbsolutePath.ToString().Split('/')[1];
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + "Route not found");
                    return;
                }
               
                
                if (req.HttpMethod == "GET")
                {
                    Log.Logger.LogMessage("Request GET -> Recive from " + req.RemoteEndPoint.ToString() + " Route -> " + route);
                    view = Routes.GetRoute(route);
                }
                else if (req.HttpMethod == "POST")
                {
                     Log.Logger.LogMessage("Request POST -> Recive from " + req.RemoteEndPoint.ToString() + " Route -> " + route);
                     view = Routes.GetRoute(route);
                }
                
               await Response(ctx, view);
            }
        }
        
        public static async Task Response(HttpListenerContext context, string view)
        {
            HttpListenerResponse resp = context.Response;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(view);
                
            resp.ContentType = "text/html";
            resp.ContentEncoding = System.Text.Encoding.UTF8;
            resp.ContentLength64 = data.LongLength;

            await resp.OutputStream.WriteAsync(data, 0, data.Length);
            resp.Close();
        }
    }
}