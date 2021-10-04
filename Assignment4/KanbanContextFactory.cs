using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Assignment4.Entities;

namespace Assignment4
{
    public class KanbanContextFactory : IDesignTimeDbContextFactory<KanbanContext>
    {
        public KanbanContext CreateDbContext(string[] args)
        {

            var connectionString = "Server=localhost;Database=Kanban;User Id=sa;Password=cb715ee5-3230-43a3-9fc2-adb1ed99d316";

            var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>()
                .UseSqlServer(connectionString);

            return new KanbanContext(optionsBuilder.Options);
        }

        public static void Seed(KanbanContext context)
        {
            context.Database.ExecuteSqlRaw("DELETE dbo.Tasks");
            context.Database.ExecuteSqlRaw("DELETE dbo.Tags");
            context.Database.ExecuteSqlRaw("DELETE dbo.Users");
            context.Database.ExecuteSqlRaw("DELETE dbo.TaskRepositories");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Tasks', RESEED, 0)");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Tags', RESEED, 0)");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Users', RESEED, 0)");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.TaskRepositories', RESEED, 0)");

            var user = new User {
                Id = 1,
                Name = "Snoop Dog",
                Email = "abc@skrrrt.com",
                Tasks = new List<Task>()
            };

            var tag = new Tag {
              Id = 1,
              Name = "Skrrrt",
              Tasks = new List<Task>()
            };

            var task = new Task {
                Id = 1,
                Title = "Work on it",
                AssignedTo = user,
                Description = "Yeeet",
                State = Core.State.Active,
                Tags = new List<Tag> { tag }
            };

            tag.Tasks.Add(task);

            context.Users.AddRange(
                user
            );

            context.SaveChanges();
        }
    }
}