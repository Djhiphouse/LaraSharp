using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;

namespace Adventofcode_day1.Sql
{
    public class SqlBuilder
    {
        public string Ip { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        
        public MySqlConnection Connection { get; set; }
        
        public SqlBuilder(string ip, string port, string user, string password, string database)
        {
            Ip = ip;
            Port = port;
            User = user;
            Password = password;
            Database = database;
            Connection = new MySqlConnection(BuildConnection());
        }
        
        private string BuildConnection()
        {
            return $"Server={Ip},{Port};Database={Database};User Id={User};Password={Password};";
        }
        
    }
    public class Sql
    {
        public string command { get; set; }
        public MySqlConnection Connection;
        public Sql(string database, MySqlConnection con)
        {
            Connection = con;
            command += $"USE {database}; ";
        }
        
        public void Create(string table, string[] columns, string[] values)
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
        }
        
        public async Task<string> Read(string table, string[] columns)
        {
            command += $"SELECT ";
            for (int i = 0; i < columns.Length; i++)
            {
                command += $"{columns[i]}, ";
            }
            command = command.Remove(command.Length - 2);
            command += $" FROM {table}; ";
            return await ExecuteRead(columns);
        }
        
        public void Open()
        {
            Connection.Open();
        }
        
        public void Close()
        {
            Connection.Close();
        }
        
        public async Task Execute()
        {
            Open();
            MySqlCommand sqlCommand = new MySqlCommand(command, Connection);
            await sqlCommand.ExecuteNonQueryAsync();
            Close();
        }
        
        public async Task<string> ExecuteRead(string[] Coluums)
        {
            string result = "";

            try
            {
                Open();
                MySqlCommand val = new MySqlCommand(command, Connection);
                MySqlDataReader Data = val.ExecuteReader();
                await ((DbDataReader)(object)Data).ReadAsync();
                if (!((DbDataReader)(object)Data).HasRows)
                {
                    return "No rows found";
                }

                foreach (var coluum in Coluums)
                {
                    result += Environment.NewLine +  Data.GetString(coluum);  
                }
                
            }
            finally
            {
                Close();
            }

            return result.TrimEnd(); // Trim the last newline for cleaner output
        }
    }
}