namespace SimpleCQRS.Domain.Events
{
    public class Event : Message
    {
        public int Version { get; set; }
    }
}
