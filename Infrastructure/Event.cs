using Application;

namespace Infrastructure
{
    public class Event : Message
    {
        public int Version { get; set; }
    }
}
