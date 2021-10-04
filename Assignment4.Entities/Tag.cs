using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment4.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Index(IsUnique=true)]
        public string Name { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
