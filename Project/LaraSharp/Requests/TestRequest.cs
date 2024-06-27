using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class MyHandlers
{
    public static async Task<string> SubmitForm(HttpListenerRequest request)
    {
        string requestBody;
        using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
        {
            requestBody = await reader.ReadToEndAsync();
        }

        // Deserialize the JSON body to an object
        var formData = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestBody);

        // Process the form data here
        Console.WriteLine("Received form data:");
        foreach (var kvp in formData)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        // Return a response
        return await Task.FromResult("<html><body><h1>Form submitted successfully</h1></body></html>");
    }
}