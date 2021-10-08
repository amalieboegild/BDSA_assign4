using System;

using Xunit;

using Assignment4.Entities;
using Assignment4.Core;


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace Assignment4.Entities.Tests
{
    public class TaskRepositoryTests
    {
        
        [Fact]
        public void CanFindById() {
            // Arrange
            var repo = GetRepo();
            
            TaskDetailsDTO expected = new TaskDetailsDTO
            {
              Id = 2,
              Title = "Test Task",
              Description = "This is a desc",
              AssignedToId = 1,

              AssignedToName = "Test",
              AssignedToEmail = "abc@gmail.com",
              Tags = new string[] {"Test Tag"},
              State = State.Active
            };


            // Act
            var actual = repo.FindById(2);

            // Assert
            Assert.Equal(expected, actual);
        }

        private TaskRepository GetRepo() {
            var connectionString = "Server=localhost;Database=Kanban;User Id=postgres;Password=1";

            var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>()
                .UseNpgsql(connectionString, b => b.MigrationsAssembly("Assignment4"));

            var ctx = new KanbanContext(optionsBuilder.Options);
            return new TaskRepository(ctx);
        }

    }
}
