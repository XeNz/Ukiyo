using System;
using System.Threading.Tasks;
using Ukiyo.Infrastructure.Ioc.Initializers;

namespace Ukiyo.Api
{
    public class StartupInitializer : IStartupInitializer
    {
        public Task InitializeAsync()
        {
            Console.WriteLine("startup init started");
            return Task.CompletedTask;
        }

        public void AddInitializer(IInitializer initializer)
        {
        }
    }
}