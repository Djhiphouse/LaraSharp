using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Adventofcode_day1.Server;
using Adventofcode_day1.Settings;
using Adventofcode_day1.Sql;

class Program
{
    static async Task Main(string[] args)
    {
        SqlBuilder builder = new SqlBuilder("localhost", "3006", "root", "ru[i43uxm0DY&4Tj}Z\"3|/5?^!R|c,8%((VQ:T_z&@tJU", "test");
        Sql sql = new Sql(builder.Database, builder.Connection);
        string conetent =  await sql.Read("base", new string[] { "test" });
        
        Console.WriteLine(conetent);
        
        Settings settings = new Settings();
        settings.Initializ();
        settings.SetSettings();
        Http http = new Http();
        http.Start();
        Thread.Sleep(-1);
    }
    
    
}

