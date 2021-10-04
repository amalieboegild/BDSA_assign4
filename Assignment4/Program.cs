using System;
using System.Data.SqlClient;

namespace Assignment4
{
    class Program
    {
        static void Main(string[] args)
        {
            //var configuration = LoadConfiguration();
            //var connectionString = configuration.GetConnectionString("Futurama");
            var connectionString = "Server=localhost;Database=Kanban;User Id=sa;Password=cb715ee5-3230-43a3-9fc2-adb1ed99d316";
            
            // using var connection = new SqlConnection(connectionString);
            // var cmdText = "SELECT * FROM Task";
            // using var command = new SqlCommand(cmdText, connection);
            // connection.Open();
            // using var reader = command.ExecuteReader();
            // while (reader.Read())
            // {
            //     var character = new
            //     {
            //         Id = reader.GetInt32("Id"),
            //         Name = reader.GetString("Name"),
            //         Species = reader.GetString("Species"),
            //         Planet = reader.GetString("Planet"),
            //     };
            //     Console.WriteLine(character);
            // }
        }
    }
}
