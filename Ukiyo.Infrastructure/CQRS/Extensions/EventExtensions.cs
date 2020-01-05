using System;
using Microsoft.Extensions.DependencyInjection;
using Ukiyo.Infrastructure.CQRS.Dispatchers;
using Ukiyo.Infrastructure.CQRS.Events;
using Ukiyo.Infrastructure.Ioc.Builders;

namespace Ukiyo.Infrastructure.CQRS.Extensions
{
    public static class EventExtensions
    {
        public static IUkiyoBuilder AddEventHandlers(this IUkiyoBuilder builder)
        {
            builder.Services.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return builder;
        }

        public static IUkiyoBuilder AddInMemoryEventDispatcher(this IUkiyoBuilder builder)
        {
            builder.Services.AddSingleton<IEventDispatcher, EventDispatcher>();
            return builder;
        }
    }
}