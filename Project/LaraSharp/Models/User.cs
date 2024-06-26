using System.Collections.Generic;
using System.Threading.Tasks;
using Adventofcode_day1.Log;
using Newtonsoft.Json;

namespace Adventofcode_day1.Models
{
    public class User
    {
        public static SqlBuilder builder;
        public static Sql sql;
        public static string table = "users";
        
        public static void Initialize(Settings.Settings settings)
        {
            builder = new SqlBuilder(settings);
            sql = new Sql(builder.Database);
        }

        public static void Migrate()
        {
            var migrate = new Migration(table)
                .Id() 
                .String("name") 
                .String("password")
                .Timestamps();
            
            migrate.Build(Sql.Connection);
        }
        
        public static async Task<string> CheckLogin(string username, string password)
        {
            Logger.LogMessage("Check login: "+ sql.
                Use("test").
                Select().
                From(table).
                Where("name", $"'{username}'").
                Where("password", $"'{password}'").
                ToSql());
            
            bool login = await sql.
                Use("test").
                Select().
                From(table).
                Where("name", $"'{username}'").
                Where("password", $"'{password}'").
                CheckExists();
            
            
            
            
            
            return login ? "true" : "false";
        }
        public static async Task<string> getUsers()
        {
            return await sql.Select().From(table).ExecuteReadAsync();
        }
        
        public static string GetUsersSynchron()
        {
            return sql.Select().From(table).Read();
        }

        public static List<Dictionary<string, object>> GetUsersForTable()
        {
            var jsonString = GetUsersSynchron();
            var users = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonString);
            return users;
        }
        
        public static async Task<List<Dictionary<string, object>>> GetUsersForTableAsync()
        {
            var jsonString = await getUsers();
            var users = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonString);
            return users;
        }

    }
}