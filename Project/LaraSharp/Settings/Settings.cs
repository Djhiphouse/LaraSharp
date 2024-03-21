using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Adventofcode_day1.Settings
{
    public class Settings
    {
        public static  Dictionary<string, string> Setting = new Dictionary<string, string>();

        public void SetSettings()
        {
            foreach (var setting in File.ReadAllLines("Settings/env.conf"))
            {
                if (setting == "") 
                    continue;
                
                string[] currentSetting = setting.Split('=');
                //Console.WriteLine("Setting Name: " + currentSetting[0] + " = " + " Set: " + currentSetting[1]);
                Setting.Add(currentSetting[0], currentSetting[1]);
            }
        }
        
        public string GetSetting(string setting)
        {
            if (Setting.ContainsKey(setting))
            {
                return Setting[setting];
            }
            Log.Logger.LogMessage("Setting -> " + setting + " not found");
            return null;
        }

        public void Initializ()
        {
            if (Directory.Exists("Settings"))
            {
                if (!File.Exists("Settings/env.conf"))
                {
                    File.Create("Settings/env.conf");
                }
            }
            else
            {
                Directory.CreateDirectory("Settings");
                File.Create("Settings/env.conf");
            }


            if (Directory.Exists("Log"))
            {
                if (!File.Exists("Log/logs.log"))
                {
                    File.Create("Log/logs.log");
                }
            }
            else
            {
                Directory.CreateDirectory("Log");
                File.Create("Log/logs.log");
            }

            if (!Directory.Exists("Icons"))
            {
                Directory.CreateDirectory("Icons");
            }
            
            if (Directory.Exists("Views"))
            {
                if (!File.Exists("Views/index.html"))
                {
                    File.WriteAllText("Views/index.html", Pages.Index);
                }
                
                if (!File.Exists("Views/error.html"))
                {
                    File.WriteAllText("Views/error.html", Pages.Error);
                }
            }else
            {
                Directory.CreateDirectory("Views");
            }

            if (Directory.Exists("Icons") && Directory.Exists("Views") && Directory.Exists("Settings") && Directory.Exists("Log"))
            {
                Log.Logger.LogMessage("Initialization -> Success");
            }
            else
            {
                Console.WriteLine("Initialization -> End with error");
                Environment.Exit(0);
            }
           
        }
    }
    
    public class Pages
    {
        public static string Index = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Welcome to LaraSharp Project</title>
    <link href=""https://cdn.jsdelivr.net/npm/tailwindcss@2.2.19/dist/tailwind.min.css"" rel=""stylesheet"">
</head>
<body class=""bg-gray-900 flex items-center justify-center min-h-screen text-white"">
<div class=""text-center p-5"">
    <h1 class=""text-5xl font-bold mb-4"">Welcome to LaraSharp Project</h1>
    <p class=""mb-8"">Embark on a journey to build a cutting-edge web application using the power of LaraSharp, a perfect blend of Laravel's elegance with C#'s strength.</p>
    <div class=""animate-bounce inline-block mb-8"">
     <h1 class=""text-green-700 text-2xl"">
         made by <a href=""https://github.com/Djhiphouse"">Djhiphouse</a>
     </h1>
    </div>
   
</div>
</body>
</html>
";
        
        public static string Error = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>404 Not Found</title>
    <link href=""https://cdn.jsdelivr.net/npm/tailwindcss@2.2.19/dist/tailwind.min.css"" rel=""stylesheet"">
</head>
<body class=""bg-gray-900 flex items-center justify-center min-h-screen text-white"">
<div class=""text-center"">
    <div class=""animate-bounce mb-6"">
        <svg class=""w-16 h-16 mx-auto text-gray-500"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""2"" viewBox=""0 0 24 24"" stroke=""currentColor"">
            <path d=""M9 20l-5.447-2.724A1 1 0 013 16.382V5.618a1 1 0 011.447-.894L9 7.118M9 20l5.447 2.724A1 1 0 0015 22h4a1 1 0 001-1v-11.382a1 1 0 00-.447-.894L9 7.118M9 20l6-3m-6-5l6 3m0 0L21 5m-6 3l6-3""></path>
        </svg>
    </div>
    <h1 class=""text-4xl font-bold mb-2"">404 Not Found</h1>
    <p class=""mb-6"">Oops! The page you're looking for doesn't exist.</p>
    <a href=""/"" class=""px-4 py-2 bg-gray-800 rounded hover:bg-gray-700"">Go Home</a>
</div>
<script src=""app.js""></script>
</body>
</html>
";
    }
}