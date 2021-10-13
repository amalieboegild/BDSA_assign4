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
    public class TagRepositoryTests : IDisposable
    {
        private readonly KanbanContext _ctx;
        private readonly TagRepository _repo;

        public TagRepositoryTests() {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<KanbanContext>();
            builder.UseSqlite(connection);

            _ctx = new KanbanContext(builder.Options);
            _ctx.Database.EnsureCreated();
            _repo = new TagRepository(_ctx);
            _ctx.SaveChanges();

            var user = new User
            {
                Name = "Snoop Dog",
                Email = "abc@skrrrt.com",
                Tasks = new List<Task>()
            };

            var tag = new Tag
            {
                Name = "Important",
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
            TagDTO expected = new TagDTO
            {
              Id = 1,
              Name = "Important",
              Tasks = new List<string> { "Work on it" },
            };


            // Act
            var actual = _repo.FindById(1);


            // Assert
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Tasks, actual.Tasks);
            
          //  Assert.Equal(expected, actual);
        }

        [Fact]
        public void Update_Name() {
            // Arrange
            TagDTO t = new TagDTO
            {
              Id = 1,
              Name = "Very important",
              Tasks = new List<string> { "" },
            };

            // Act
            _repo.Update(t);


            // Assert
            Assert.Equal("Very important", _repo.FindById(1).Name);
            
        }

        public void Dispose() {
            _ctx.Dispose();
        }

        [Fact]
        public void canCreate() {
            // Arrange
            TagDTO expected = new TagDTO
            {
              Id = 2,
              Name = "Very important",
              Tasks = new List<string> { "Work on it" }
            };


            // Act
            var actual = _repo.Create(expected);


            // Assert
            Assert.Equal(expected.Id, actual);
        }

        [Fact]
        public void canDelete() {

            // Act
            _repo.Delete(1);


            // Assert
            Assert.Throws<InvalidOperationException>(()=>{
                _repo.FindById(1);
            });
        }

    }
}
