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
                .AddText("Welcome to LaraSharp", "lara_str")
                .AddActionButton("Add Log", async () => await EditDiv(), ButtonStyle.Primary)
                .AddLiveTable("api/logs", new[] { "email" })
                .EndCenter()
                .EndPage()
                .GetHTML();
        }
        
    }
}