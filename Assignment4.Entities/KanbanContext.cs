using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Assignment4.Core;
namespace Assignment4.Entities
{
    public class KanbanContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        public KanbanContext(DbContextOptions<KanbanContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Rules for entity creation for DB
            builder.Entity<Tag>()
                .HasIndex(t => t.Name)
                .IsUnique();

            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            
            builder
                .Entity<Task>()
                .Property(e => e.State)
                .HasConversion(new EnumToStringConverter<State>());

            // builder.Entity<TagTask>()
            //     .HasKey(t => new { t.PostId, t.TagId });

            // builder.Entity<TagTask>()
            //     .HasOne(pt => pt.Post)
            //     .WithMany(p => p.PostTags)
            //     .HasForeignKey(pt => pt.PostId);

            // builder.Entity<TagTask>()
            //     .HasOne(pt => pt.Tag)
            //     .WithMany(t => t.PostTags)
            //     .HasForeignKey(pt => pt.TagId);

            //this.SeedData(builder);
        }

        private void SeedData(ModelBuilder builder) {
            var user = new User
            {
                Id = 1,
                Name = "Snoop Dog",
                Email = "abc@skrrrt.com",
                Tasks = new List<Task>()
            };


            var task = new Task {
                Id = 1,
                Title = "Work on it",
                AssignedTo = user,
                Description = "Yeeet",
                State = Core.State.Active,
                Tags = new List<Tag>()
            };


            var tag = new Tag
            {
                Id = 1,
                Name = "Skrrrt",
                Tasks = new List<Task> ()
            };

            //tag.Tasks.Add(task);

            // builder
            //     .Entity<Tag>()
            //     .HasMany(tag => tag.Tasks)
            //     .WithMany(task => task.Tags)
            //     .UsingEntity<Dictionary<string, object>>(
            //         "TagTask",
            //         j => j
            //             .HasOne<Task>()
            //             .WithMany()
            //             .HasForeignKey("TasksId"),
            //         j => j
            //             .HasOne<Tag>()
            //             .WithMany()
            //             .HasForeignKey("TagsId")
            //     );
            

            builder
                .Entity<Tag>()
                .HasMany(tag => tag.Tasks)
                .WithMany(task => task.Tags)
                .UsingEntity(j => j.HasData(
                    new {
                        TagsId = tag.Id,
                        TasksId = task.Id
                    }
                ));
            builder.Entity<Tag>().HasData(tag);
            builder.Entity<Task>().HasData(task);
            builder
                .Entity<User>()
                .HasMany(u => u.Tasks)
                .WithOne(task => task.AssignedTo)
                .HasForeignKey("AssignedToId");
            builder
                .Entity<User>()
                .HasData(user);
            

            /*builder.Entity<Task>(
                t => {
                    t.HasData(task);
                    t.OwnsMany(e => e.Tags)
                    .HasData(tag);
                }
            );*/

        }
    }
}
