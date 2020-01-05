using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Ukiyo.Infrastructure.Ioc.Initializers;

namespace Ukiyo.Infrastructure.Ioc.Builders
{
    public class UkiyoBuilder : IUkiyoBuilder
    {
        private readonly List<Action<IServiceProvider>> _buildActions;
        private readonly List<string> _registry;
        private readonly IServiceCollection _services;

        private UkiyoBuilder(IServiceCollection services)
        {
            _registry = new List<string>();
            _buildActions = new List<Action<IServiceProvider>>();
            _services = services;
        }

        IServiceCollection IUkiyoBuilder.Services => _services;

        public bool TryRegister(string name)
        {
            var isAlreadyRegistered = _registry.Any(r => r == name);

            if (isAlreadyRegistered) return false;

            _registry.Add(name);
            return true;
        }

        public void AddBuildAction(Action<IServiceProvider> execute)
        {
            _buildActions.Add(execute);
        }

        public void AddInitializer(IInitializer initializer)
        {
            AddBuildAction(sp =>
            {
                var startupInitializer = sp.GetService<IStartupInitializer>();
                startupInitializer.AddInitializer(initializer);
            });
        }

        public void AddInitializer<TInitializer>() where TInitializer : IInitializer
        {
            AddBuildAction(sp =>
            {
                var initializer = sp.GetService<TInitializer>();
                var startupInitializer = sp.GetService<IStartupInitializer>();
                startupInitializer.AddInitializer(initializer);
            });
        }

        public IServiceProvider Build()
        {
            var serviceProvider = _services.BuildServiceProvider();
            _buildActions.ForEach(a => a(serviceProvider));
            return serviceProvider;
        }

        public static IUkiyoBuilder Create(IServiceCollection services)
        {
            return new UkiyoBuilder(services);
        }
    }
}