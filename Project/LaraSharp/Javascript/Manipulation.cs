using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Adventofcode_day1.Log;
using Adventofcode_day1.Settings;

namespace Adventofcode_day1.Javascript
{
    public class DomEditor
    {
        private static Queue<string> commandQueue = new Queue<string>();
        private static HttpListener listener = new HttpListener();

        public DomEditor()
        {
            listener.Prefixes.Add("http://" + Instance.Host + ":8001/");
        }

        public async Task EditDomListener()
        {
            listener.Start();
            Instance.debugger.DebugInfo($@"Server started on http://{Instance.Host}:8001", "DOM EDIT LISTENER");
            while (true)
            {
                var context = await listener.GetContextAsync();
                var response = context.Response;
                response.Headers.Add("Access-Control-Allow-Origin", "*");  // Handle CORS here

                string command = commandQueue.Count > 0 ? commandQueue.Dequeue() : "Queue is empty";

                byte[] buffer = Encoding.UTF8.GetBytes(command);
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }
        }


        public void AddToQueue(string elementById, string value)
        {
            string command = $"document.getElementById('{elementById}').innerText = '{value}';";
            commandQueue.Enqueue(command);
            Instance.debugger.DebugInfo($"Edit Element -> " + elementById,"LIVE REFRESH");
        }
    }
}