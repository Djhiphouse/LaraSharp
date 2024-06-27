using System;
using System.Threading.Tasks;
using Adventofcode_day1.Html;
using Adventofcode_day1.Javascript;
using Adventofcode_day1.Models;
using Adventofcode_day1.Settings;

namespace Adventofcode_day1.Views
{
    public class LogsView
    {
        public static HtmlBuilder htmlBuilder;
        
        
        public static async Task EditDiv()
        {
             Console.WriteLine("Refresh Live");
             Instance.domdocument.AddToQueue("lara_str", "Proof of live refresh");
             Instance.domdocument.AddJavascript(htmlBuilder.AddLiveNotification("test", "test"));
             Instance.Route.Redirect("/welcome");
        }

        public static void Initialize()
        {
            var users = Logs.GetLogsForTable();
            htmlBuilder = new HtmlBuilder();
            htmlBuilder
                .Build()
                .AddHead()
                .Page()
                .Center()
                .Form("/submitForm") // The URL where the form data will be sent
                .AddInput("Name", "Enter your name", "name")
                .AddTextArea("Message", "Enter your message", "message")
                .EndForm()
                .EndCenter()
                .EndPage()
                .GetHTML();
        }
        
    }
}