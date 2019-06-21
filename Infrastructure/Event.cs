using Application;
using MediatR;

namespace Infrastructure
{
    public class Event : INotification
    {
        public int Version { get; set; }
    }
}
