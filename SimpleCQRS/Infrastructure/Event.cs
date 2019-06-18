using SimpleCQRS.Application;

namespace SimpleCQRS.Infrastructure
{
    public class Event : Message
    {
        public int Version { get; set; }
    }
}
