using System;
using System.Management.Instrumentation;
using Adventofcode_day1.Html;
using Adventofcode_day1.Javascript;
using Adventofcode_day1.Models;

namespace Adventofcode_day1.Views
{
    public class Login
    {
        public static HtmlBuilder htmlBuilder;
        public static void EditText()
        {
           new DomEditor().AddToQueue("lara_str", "Refreshed Text");
        }
        
        
        
        public static void Initialize()
        {
            var users =  User.GetUsersForTable();
            htmlBuilder = new HtmlBuilder();
            htmlBuilder
                .Build()
                 .AddHead()
                  .Page()
                   .Center()
                   .AddText("Welcome to LaraSharp", "lara_str")
                    .AddActionButton("Live Change", EditText, ButtonStyle.Primary)
                    .AddLiveTable("api/usertable" ,new []{"email"})
                  .EndCenter()
                 .EndPage()
                .GetHTML();
        }
    }
}