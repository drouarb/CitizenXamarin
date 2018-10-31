using System;

namespace citizen.Models
{
    public class PollItem
    {
        public Guid uuid { get; set; }
        public DateTime end { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}