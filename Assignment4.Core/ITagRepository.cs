using System;
using System.Collections.Generic;

namespace Assignment4.Core
{
    public interface ITagRepository : IDisposable
    {
        IReadOnlyCollection<TagDTO> All();

        /// <summary>
        ///
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>The id of the newly created tag</returns>
        int Create(TagDTO tag);
        
        void Delete(int tagId);

        TagDTO FindById(int id);

        void Update(TagDTO tag);
    }
}
