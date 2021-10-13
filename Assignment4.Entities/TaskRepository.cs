using System.Collections.Generic;
using Assignment4.Core;
using System.Linq;

namespace Assignment4.Entities
{
    public class TaskRepository: ITaskRepository
    {
        private readonly KanbanContext dbContext;

        public TaskRepository(KanbanContext context)
        {
            dbContext = context;
        }

        public IReadOnlyCollection<TaskDTO> All()
        {
            return dbContext.Tasks.Select(x => new TaskDTO
            {
                Id = x.Id,
                Title = x.Title,
                AssignedToId = x.AssignedTo != null ? x.AssignedTo.Id : null,
                Description = x.Description,
                State = x.State,
                Tags = x.Tags.Select(t => t.Name).ToList()
            }).ToList();

        }

        public int Create(TaskDTO task)
        {
            User user = dbContext.Users.SingleOrDefault(u => u.Id == task.AssignedToId.GetValueOrDefault());

            dbContext.Tasks.Add(new Task
            {
                Title = task.Title,
                AssignedTo = user,
                Description = task.Description,
                State = State.New,
                Tags = dbContext.Tags.Where(x => task.Tags.Contains(x.Name)).ToList()
            });

            return dbContext.SaveChanges();
            
        }

        public Response Delete(int taskId)
        {
            Task task = dbContext.Tasks.Single(t => t.Id == taskId);
            if(task.State == State.New)
            {
                dbContext.Tasks.Remove(task);
                dbContext.SaveChanges();  
                return Response.Deleted;
            } else if(task.State == State.Active)
            {
                task.State = State.Removed;
                dbContext.SaveChanges();  
                return Response.Updated;
            }  
                
            return Response.Conflict;   
            
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public TaskDetailsDTO FindById(int id) 
        {
            Task task = dbContext.Tasks.Single(t => t.Id == id);
            
            return new TaskDetailsDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                AssignedToId = task.AssignedTo.Id,

                AssignedToName = task.AssignedTo.Name,
                AssignedToEmail = task.AssignedTo.Email,
                Tags = task.Tags.Select(tag => tag.Name).ToList(),
                State = task.State
            };
        }

        public void Update(TaskDTO task)
        {
            Task dbTask = dbContext.Tasks.Single(x => x.Id == task.Id);
            User dbUser = dbContext.Users.SingleOrDefault(x => x.Id == task.AssignedToId.GetValueOrDefault());

            dbTask.Id = task.Id;
            dbTask.Title = task.Title;
            dbTask.Description = task.Description;
            dbTask.AssignedTo = dbUser;
            dbTask.State = task.State;
            dbTask.Tags = dbContext.Tags.Where(x => task.Tags.Contains(x.Name)).ToList();

            dbContext.SaveChanges();
        }
    }
}
