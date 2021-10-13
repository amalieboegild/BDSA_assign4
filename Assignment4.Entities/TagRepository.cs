using System.Collections.Generic;
using Assignment4.Core;
using System.Linq;

namespace Assignment4.Entities
{
    public class TagRepository: ITagRepository
    {
        private readonly KanbanContext dbContext;

        public TagRepository(KanbanContext context)
        {
            dbContext = context;
        }

        public IReadOnlyCollection<TagDTO> All()
        {
            return dbContext.Tags.Select(x => new TagDTO
            {
                Id = x.Id,
                Name = x.Name,
                Tasks = x.Tasks.Select(t => t.Title).ToList()
            }).ToList();

        }

        public int Create(TagDTO tag)
        {
            dbContext.Tags.Add(new Tag
            {
                Id = tag.Id,
                Name = tag.Name,
                Tasks = dbContext.Tasks.Where(x => tag.Tasks.Contains(x.Title)).ToList()
            });

            return dbContext.SaveChanges();
            
        }

        public void Delete(int tagId)
        {
            Tag tag = dbContext.Tags.Single(t => t.Id == tagId);
            dbContext.Tags.Remove(tag);

            dbContext.SaveChanges();   
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public TagDTO FindById(int id) 
        {
            Tag tag = dbContext.Tags.Single(t => t.Id == id);
            
            return new TagDTO
            {
                Id = tag.Id,
                Name = tag.Name,
                Tasks = tag.Tasks.Select(task => task.Title).ToList(),
            };
        }

        public void Update(TagDTO tag)
        {
            Tag dbTag = dbContext.Tags.Single(x => x.Id == tag.Id);

            dbTag.Id = tag.Id;
            dbTag.Name = tag.Name;
            dbTag.Tasks = dbContext.Tasks.Where(x => tag.Tasks.Contains(x.Title)).ToList();

            dbContext.SaveChanges();
        }
    }
}
