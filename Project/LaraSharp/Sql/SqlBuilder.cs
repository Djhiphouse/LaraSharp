using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Adventofcode_day1.Settings;
using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;
using Newtonsoft.Json;

namespace Adventofcode_day1.Models
{
    public class SqlBuilder
    {
        public string Ip { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        
        public static MySqlConnection Connection { get; set; }
        
        public SqlBuilder(Settings.Settings settings)
        {
            Ip = settings.GetSetting("DB_HOST");
            Port = settings.GetSetting("PORT"); 
            User = settings.GetSetting("DB_USER");
            Password = settings.GetSetting("DB_PASS");
            Database = settings.GetSetting("DB_DATABASE");
    
            Connection = new MySqlConnection(BuildConnection());
            Connection.Open();
            if (Connection.State == System.Data.ConnectionState.Closed)
            {
                Instance.debugger.DebugError("Failed to connect to the database -> " + Database, "SQL");
                return;
            }else
             Instance.debugger.DebugSuccess("Connected to the database -> " + Database, "SQL");
        }

        
        private string BuildConnection()
        {
            return $"server={Ip};port=3306;uid={User};pwd={Password};database={Database};Max Pool Size=9000;Pooling=true;";
        }
        
    }
    public class Sql
    {
        private StringBuilder commanBuilder = new StringBuilder();
        public string command { get; set; }
        public static MySqlConnection Connection;
        public Sql(string database)
        {
            Connection = SqlBuilder.Connection;
            command += $"USE {database}; ";
        }
        
        
        public async Task Create(string table, string[] columns, string[] values)
        {
            command += $"INSERT INTO {table} (";
            for (int i = 0; i < columns.Length; i++)
            {
                command += $"{columns[i]}, ";
            }
            command = command.Remove(command.Length - 2);
            command += ") VALUES (";
            for (int i = 0; i < values.Length; i++)
            {
                command += $"'{values[i]}', ";
            }
            command = command.Remove(command.Length - 2);
            command += "); ";
            
            MySqlCommand sqlCommand = new MySqlCommand(command, Connection);
            sqlCommand.ExecuteNonQuery();
            await Close();
        }
        
        
        public async Task Open()
        {
            await Connection.OpenAsync();
        }
        
        public async Task Close()
        {
           await Connection.CloseAsync();
        }
        
        public Sql Use(string db = "*")
        {
            commanBuilder.Append($"USE {db};");
            return this;
        }
        public Sql Select(string columns = "*")
        {
            commanBuilder.Append($"SELECT {columns} ");
            return this;
        }

        public Sql From(string tableName)
        {
            commanBuilder.Append($"FROM {tableName} ");
            return this;
        }

        public Sql Where(string value, string condition)
        {
            if (commanBuilder.ToString().Contains("WHERE"))
            {
                commanBuilder.Append($"AND {value}={condition} ");
                return this;
            }
            
            commanBuilder.Append($"WHERE {value}={condition} ");
            return this;
        }

        
        public Sql Insert(string table, string[] columns, string[] values)
        {
            commanBuilder.Append($"INSERT INTO {table} (");
            for (int i = 0; i < columns.Length; i++)
            {
                commanBuilder.Append($"{columns[i]}, ");
            }
            commanBuilder.Remove(commanBuilder.Length - 2, 2);
            commanBuilder.Append(") VALUES (");
            for (int i = 0; i < values.Length; i++)
            {
                commanBuilder.Append($"'{values[i]}', ");
            }
            commanBuilder.Remove(commanBuilder.Length - 2, 2);
            commanBuilder.Append(") ");
            return this;
        }
        
        public Sql InnerJoin(string table, string condition)
        {
            commanBuilder.Append($"INNER JOIN {table} ON {condition} ");
            return this;
        }

        public Sql AddCustomClause(string customClause)
        {
            commanBuilder.Append($"{customClause} ");
            return this;
        }

        
        public string ToSql()
        {
            return commanBuilder.Append(";").ToString().TrimEnd();
        }
        
        
        public async Task Execute()
        {
            await Open();
            MySqlCommand sqlCommand = new MySqlCommand(command, Connection);
            await sqlCommand.ExecuteNonQueryAsync();
            await Close();
        }
        
        public void ExecuteSync()
        {
            Connection.Open();
            MySqlCommand sqlCommand = new MySqlCommand(command, Connection);
            sqlCommand.ExecuteNonQuery();
            Connection.Close();
        }
        
        
        public string Read()
        {
            var result = new List<Dictionary<string, object>>();
    
            if (Connection.State == System.Data.ConnectionState.Closed)
                Connection.Open();
    
            MySqlCommand sqlCommand = new MySqlCommand(ToSql(), Connection);
            using (var reader = sqlCommand.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    return "[]"; // Return an empty JSON array if no rows found
                }

                while (reader.Read())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var columnValue = reader.IsDBNull(i) ? null : reader.GetValue(i);
                        row[columnName] = columnValue;
                    }
                    result.Add(row);
                }
            }

            Connection.Close();
            return JsonConvert.SerializeObject(result);
        }

        
        public async Task<string> ExecuteReadAsync()
        {
            StringBuilder result = new StringBuilder();

            try
            {
                await Open(); // Assuming OpenAsync method exists for opening connection asynchronously
                using (var cmd = new MySqlCommand(ToSql(), Connection))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (!reader.HasRows)
                    {
                        return "No rows found.";
                    }

                    while (await reader.ReadAsync())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var columnName = reader.GetName(i); // Get the column name if needed
                            var columnValue = reader.IsDBNull(i) ? "NULL" : reader.GetString(i);
                            result.AppendLine($"{columnName}: {columnValue}");
                        }
                        result.AppendLine(); // Add an extra line between rows for clarity
                    }
                }
            }
            finally
            {
                await Close(); // Assuming CloseAsync method exists for closing connection asynchronously
            }

            return result.ToString().TrimEnd();
        }

        public async Task<bool> CheckExists()
        {
            try
            {
                await Open(); // Assuming OpenAsync method exists for opening connection asynchronously
                using (var cmd = new MySqlCommand(ToSql(), Connection))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (!reader.HasRows)
                    {
                        return false;
                    }

                    return true;
                }
            }
            finally
            {
                await Close(); // Assuming CloseAsync method exists for closing connection asynchronously
            }
        }
    }
    
    public class Migration
    {
        private readonly List<string> _columns = new List<string>();
        private readonly string _tableName;

        public Migration(string tableName)
        {
            if(SqlBuilder.Connection.State == System.Data.ConnectionState.Closed)
                SqlBuilder.Connection.Open();
            _tableName = tableName;
        }

        public Migration Id(string columnName = "id")
        {
            _columns.Add($"{columnName} INT AUTO_INCREMENT PRIMARY KEY");
            return this;
        }

        public Migration String(string columnName, int length = 255)
        {
            _columns.Add($"{columnName} VARCHAR({length})");
            return this;
        }
        
        public Migration Int(string columnName, int length = 255)
        {
            _columns.Add($"{columnName} INTEGER({length})");
            return this;
        }

        // Method to add timestamps (created_at and updated_at)
        public Migration Timestamps()
        {
            _columns.Add("created_at DATETIME");
            _columns.Add("updated_at DATETIME");
            return this;
        }
        
        public bool HasTable(string tableName, MySqlConnection con)
        {
            string command = $"SHOW TABLES LIKE '{tableName}'";
            MySqlCommand sqlCommand = new MySqlCommand(command, con);
            var reader = sqlCommand.ExecuteReader();
            if (!reader.HasRows)
            {
                return false;
            }
            
            return true;
        }

        // Method to generate the final SQL command
        public void Build(MySqlConnection con)
        {
                string command = $"CREATE TABLE IF NOT EXISTS {_tableName} (";
                command += string.Join(", ", _columns);
                command += ");";
                MySqlCommand sqlCommand = new MySqlCommand(command, con);
                sqlCommand.ExecuteNonQuery();
                con.Close();
        }
    }
}