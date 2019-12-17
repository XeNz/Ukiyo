using System;
using Microsoft.Extensions.DependencyInjection;
using Ukiyo.Infrastructure.Ioc.Initializers;

namespace Ukiyo.Infrastructure.Ioc.Builders
{
    public interface IUkiyoBuilder
    {
        IServiceCollection Services { get; }
        bool TryRegister(string name);
        void AddBuildAction(Action<IServiceProvider> execute);
        void AddInitializer(IInitializer initializer);
        void AddInitializer<TInitializer>() where TInitializer : IInitializer;
        IServiceProvider Build();
    }
}