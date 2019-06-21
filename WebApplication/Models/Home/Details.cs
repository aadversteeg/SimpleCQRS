using System;

namespace WebApplication.Models.Home
{
    public class Details
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public int CurrentCount { get; set; }
        public int Version { get; internal set; }
    }
}
