using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Sqlite;

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
            var connection = new SqliteConnection("Filename=local.db");
            connection.Open();
            var builder = new DbContextOptionsBuilder<KanbanContext>();
            builder.UseSqlite(connection);

            var _ctx = new KanbanContext(builder.Options);
            _ctx.Database.EnsureCreated();
            return _ctx;
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