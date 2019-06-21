using Application;
using Application.Commands;

namespace Infrastructure.InMemory
{
    public interface ICommandSender
    {
        void Send(Command command);
    }
}
