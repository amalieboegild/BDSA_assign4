using System.Collections.Generic;
using Assignment4.Core;
using System.Linq;

namespace Assignment4.Entities
{
    public class TagRepository
    {
        private readonly KanbanContext dbContext;

        public TagRepository(KanbanContext context)
        {
            dbContext = context;
        }

    }
}
