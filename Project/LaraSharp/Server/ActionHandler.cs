using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Adventofcode_day1.Settings;

public class HttpServer
{
    private static readonly HttpServer instance = new HttpServer();
    private Dictionary<string, Func<Task>> routeActions = new Dictionary<string, Func<Task>>();

    public HttpServer()
    {
        AddFunction("test", () => Task.Run(() => Console.WriteLine("Test action executed.")));
    }

    public void AddFunction(string name, Func<Task> action)
    {
        routeActions[name] = action;
    }

    public void AddFunction(string name, Action action)
    {
        routeActions[name] = () => Task.Run(action);
    }

    public async Task StartServer()
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://" + Instance.Host + ":8080/");
        listener.Start();
        Instance.debugger.DebugInfo($@"Server started on http://{Instance.Host}:8080", "HTTP SERVER");

        try
        {
            while (true)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                SetupCors(response);

                if (request.HttpMethod == "OPTIONS")
                {
                    response.StatusCode = 204; // No Content
                    response.Close();
                    continue;
                }

                Instance.debugger.DebugSuccess("Request on Route -> " + request.Url.AbsolutePath, "HTTP SERVER Request");

                string actionKey = request.Url.AbsolutePath.TrimStart('/');
                if (routeActions.ContainsKey(actionKey))
                {
                    await routeActions[actionKey]();
                    SendResponse(response, "Action executed successfully.");
                    Instance.debugger.DebugSuccess("Action executed successfully.", "HTTP SERVER ACTION");
                }
                else
                {
                    Console.WriteLine("Invalid action: " + actionKey);
                    Instance.debugger.DebugError("Invalid action: " + actionKey, "HTTP SERVER ACTION");
                    SendResponse(response, "Invalid action", 404);
                }

                response.OutputStream.Close();
            }
        }
        catch (Exception e)
        {
            Instance.debugger.DebugError("HTTP server error: " + e.Message, "HTTP SERVER ERROR");
            listener.Stop();
        }
    }

    private void SetupCors(HttpListenerResponse response)
    {
        response.AppendHeader("Access-Control-Allow-Origin", "*");
        response.AppendHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        response.AppendHeader("Access-Control-Allow-Headers", "Content-Type");
    }

    private void SendResponse(HttpListenerResponse response, string message, int statusCode = 200)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        response.ContentType = "text/plain";
        response.StatusCode = statusCode;
        response.ContentLength64 = buffer.Length;
        response.OutputStream.Write(buffer, 0, buffer.Length);
    }
}
