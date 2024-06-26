using System.Collections.Generic;
using System.Threading.Tasks;
using Adventofcode_day1.Models;
using Newtonsoft.Json;

namespace Adventofcode_day1.Models
{
    public class Logs
    {
        public static SqlBuilder builder;
        public static Sql sql;
        public static string table = "logs";
        
        public static void Initialize(Settings.Settings settings)
        {
            builder = new SqlBuilder(settings);
            sql = new Sql(builder.Database);
        }
        
        public static void Migrate()
        {
            var migrate = new Migration(table)
                .Id() 
                .String("message") 
                .Timestamps();
            
            migrate.Build(Sql.Connection);
        }
        
        public static Task<string> getlogs()
        {
            return sql.Select().From(table).ExecuteReadAsync();
        }
        
        public static string GetLogsSynchron()
        {
            return sql.Select().From(table).Read();
        }
        
        public static List<Dictionary<string, object>> GetLogsForTable()
        {
            var jsonString = GetLogsSynchron();
            var users = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonString);
            return users;
        }
        
        public static async Task<string> GetLogsForTableAsync()
        {
            var logs = await Logs.sql.Select("*").From("logs").ExecuteReadAsync();
            return logs;
        }

    }
}