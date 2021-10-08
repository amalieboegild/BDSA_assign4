using System;
using System.Data.SqlClient;
using Assignment4.Entities;

namespace Assignment4
{
    class Program
    {
        static void Main(string[] args)
        {
            KanbanContextFactory factory = new KanbanContextFactory();
            KanbanContext kc = factory.CreateDbContext(args);

            KanbanContextFactory.Seed(kc);

            


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
