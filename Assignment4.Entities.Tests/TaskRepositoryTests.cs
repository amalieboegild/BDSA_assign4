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
            connection.Open();
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

            var tag = new Tag
            {
                Name = "Skrrrt",
                Tasks = new List<Task>()
            };

            var task = new Task
            {
                Title = "Work on it",
                AssignedTo = user,
                Description = "Yeeet",
                State = Core.State.Active,
                Tags = new List<Tag> ()
            };
            user.Tasks.Add(task);
            tag.Tasks.Add(task);
            task.Tags.Add(tag);

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
              Id = 1,
              Title = "Work on it",
              Description = "Yeeet",
              AssignedToId = 1,

              AssignedToName = "Snoop Dog",
              AssignedToEmail = "abc@skrrrt.com",
              Tags = new List<string> { "Skrrrt" },
              State = State.Active
            };


            // Act
            var actual = _repo.FindById(1);


            // Assert
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.AssignedToId, actual.AssignedToId);
            Assert.Equal(expected.AssignedToName, actual.AssignedToName);
            Assert.Equal(expected.AssignedToEmail, actual.AssignedToEmail);
            Assert.Equal(expected.Tags, actual.Tags);
            Assert.Equal(expected.State, actual.State);
        }

        public void Dispose() {
            _ctx.Dispose();
        }

    }
}
