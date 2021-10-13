using System.Collections.Generic;

namespace Assignment4.Core
{
    public record TagDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public IReadOnlyCollection<string> Tasks { get; init; }
    }
}