using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Assignment4.Entities;

namespace Assignment4
{
    public class KanbanContextFactory : IDesignTimeDbContextFactory<KanbanContext>
    {
        public KanbanContext CreateDbContext(string[] args)
        {

            var connectionString = "Server=localhost;Database=Kanban;User Id=postgres;Password=1";

            var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>()
                .UseNpgsql(connectionString, b => b.MigrationsAssembly("Assignment4"));

            return new KanbanContext(optionsBuilder.Options);
        }

        public static void Seed(KanbanContext context)
        {
            var user = new User {
                Name = "Snoop Dog",
                Email = "abc@rrt.com",
                Tasks = new List<Task>()
            };

            var tag = new Tag {
              Name = "Skrrrt",
              Tasks = new List<Task>()
            };

            var task = new Task {
                Title = "Work on it",
                AssignedTo = user,
                Description = "Yeeet",
                State = Core.State.Active,
                Tags = new List<Tag> { tag }
            };
            user.Tasks.Add(task);
            tag.Tasks.Add(task);

            context.Users.AddRange(
                user
            );

            context.SaveChanges();
        }
    }
}