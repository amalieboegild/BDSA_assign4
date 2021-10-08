using System;

using Xunit;

using Assignment4.Entities;
using Assignment4.Core;

using System.Collections.Generic;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace Assignment4.Entities.Tests
{
    public class TaskRepositoryTests : IDisposable
    {
        private readonly KanbanContext _ctx;
        private readonly TaskRepository _repo;

        public TaskRepositoryTests() {
            var connection = new SqliteConnection("Filename=:memory:");
            var builder = new DbContextOptionsBuilder<KanbanContext>();
            builder.UseSqlite(connection);

            _ctx = new KanbanContext(builder.Options);
            _ctx.Database.EnsureCreated();
            _repo = new TaskRepository(_ctx);
            _ctx.SaveChanges();

            var user = new User
            {
                Name = "Snoop Dog",
                Email = "abc@skrrrt.com",
                Tasks = new List<Task>()
            };

            /*var tag = new Tag
            {
                Name = "Skrrrt",
                Tasks = new List<Task>()
            };*/

            var task = new Task
            {
                Title = "Work on it",
                AssignedTo = user,
                Description = "Yeeet",
                State = Core.State.Active,
                Tags = new List<Tag> ()
            };
            user.Tasks.Add(task);
            //tag.Tasks.Add(task);

            _ctx.Users.AddRange(
                user
            );

            _ctx.SaveChanges();
        }

        [Fact]
        public void CanFindById() {
            // Arrange

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
            var actual = _repo.FindById(2);

            // Assert
            Assert.Equal(expected, actual);
        }

        public void Dispose() {
            _ctx.Dispose();
        }

    }
}
