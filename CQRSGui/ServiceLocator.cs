using SimpleCQRS;
using SimpleCQRS.Infrastructure.InMemory;

namespace CQRSGui
{
    public static class ServiceLocator
    {
        public static FakeBus Bus { get; set; }
       
    }
}