using SimpleCQRS.Application;
using SimpleCQRS.Application.Commands;

namespace SimpleCQRS.Infrastructure.InMemory
{
    public interface ICommandSender
    {
        void Send<T>(T command) where T : Command;
    }
}
