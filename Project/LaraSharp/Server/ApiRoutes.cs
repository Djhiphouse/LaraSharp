using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Adventofcode_day1.Settings;
using Newtonsoft.Json;

namespace Adventofcode_day1.Javascript
{
    public class ApiRouter
    {
        private static readonly Dictionary<string, Func<HttpListenerRequest, Task<string>>> routes = new Dictionary<string, Func<HttpListenerRequest, Task<string>>>();
        private static readonly HttpListener listener = new HttpListener();
        private static bool isRunning = false;

        public ApiRouter()
        {
            Instance.debugger.DebugInfo($@"USE http://{Instance.Host}:8002", "API ROUTER INFO");
            listener.Prefixes.Add("http://" + Instance.Host + ":8002/");
        }

        public async Task StartServerAsync()
        {
            listener.Start();
            isRunning = true;
            Instance.debugger.DebugInfo($@"Server started on http://{Instance.Host}:8002", "API ROUTER");
            

            while (isRunning)
            {
                try
                {
                    var context = await listener.GetContextAsync();
                    await HandleRequest(context);
                }
                catch (HttpListenerException ex)
                {
                    if (ex.ErrorCode != 995) // The I/O operation has been aborted because of either a thread exit or an application request.
                        Console.WriteLine($"HttpListenerException: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
        }

        public void StopServer()
        {
            isRunning = false;
            listener.Stop();
            Console.WriteLine("Server stopped.");
        }

        private async Task HandleRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;
            response.Headers.Add("Access-Control-Allow-Origin", "*"); // Handle CORS here

            string responseString;
            if (routes.TryGetValue(request.Url.AbsolutePath, out var handler))
            {
                responseString = await handler(request);
            }
            else
            {
                responseString = "404 Not Found";
                response.StatusCode = (int)HttpStatusCode.NotFound;
            }

            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }

        public void AddApi(string path, Func<HttpListenerRequest, Task<string>> handler)
        {
            if (!routes.ContainsKey(path))
            {
                routes[path] = handler;
                Console.WriteLine($"API route added: {path}");
            }
            else
            {
                Console.WriteLine($"API route already exists: {path}");
            }
        }
    }
}
