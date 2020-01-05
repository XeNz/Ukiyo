using System;
using Microsoft.Extensions.DependencyInjection;
using Ukiyo.Infrastructure.CQRS.Dispatchers;
using Ukiyo.Infrastructure.CQRS.Queries;
using Ukiyo.Infrastructure.Ioc.Builders;

namespace Ukiyo.Infrastructure.CQRS.Extensions
{
    public static class QueryExtensions
    {
        public static IUkiyoBuilder AddQueryHandlers(this IUkiyoBuilder builder)
        {
            builder.Services.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return builder;
        }

        public static IUkiyoBuilder AddInMemoryQueryDispatcher(this IUkiyoBuilder builder)
        {
            builder.Services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            return builder;
        }
    }
}