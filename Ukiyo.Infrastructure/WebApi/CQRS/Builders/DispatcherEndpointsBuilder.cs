using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Ukiyo.Infrastructure.CQRS.Commands;
using Ukiyo.Infrastructure.CQRS.Dispatchers;
using Ukiyo.Infrastructure.CQRS.Queries;

namespace Ukiyo.Infrastructure.WebApi.CQRS.Builders
{
    public class DispatcherEndpointsBuilder : IDispatcherEndpointsBuilder
    {
        private readonly IEndpointsBuilder _builder;

        public DispatcherEndpointsBuilder(IEndpointsBuilder builder)
        {
            _builder = builder;
        }

        public IDispatcherEndpointsBuilder Get(string path, Func<HttpContext, Task> context = null,
            Action<IEndpointConventionBuilder> endpoint = null)
        {
            _builder.Get(path, context, endpoint);

            return this;
        }

        public IDispatcherEndpointsBuilder Get<TQuery, TResult>(string path,
            Func<TQuery, HttpContext, Task> beforeDispatch = null,
            Func<TQuery, TResult, HttpContext, Task> afterDispatch = null,
            Action<IEndpointConventionBuilder> endpoint = null) where TQuery : class, IQuery<TResult>
        {
            _builder.Get<TQuery>(path, async (query, ctx) =>
            {
                if (beforeDispatch is {}) await beforeDispatch(query, ctx);

                var dispatcher = ctx.RequestServices.GetRequiredService<IQueryDispatcher>();
                var result = await dispatcher.QueryAsync<TQuery, TResult>(query);
                if (afterDispatch is null)
                {
                    if (result is null)
                    {
                        ctx.Response.StatusCode = 404;
                        return;
                    }

                    await ctx.Response.WriteJsonAsync(result);
                    return;
                }

                await afterDispatch(query, result, ctx);
            }, endpoint);

            return this;
        }

        public IDispatcherEndpointsBuilder Post(string path, Func<HttpContext, Task> context = null,
            Action<IEndpointConventionBuilder> endpoint = null)
        {
            _builder.Post(path, context, endpoint);

            return this;
        }

        public IDispatcherEndpointsBuilder Post<T>(string path, Func<T, HttpContext, Task> beforeDispatch = null,
            Func<T, HttpContext, Task> afterDispatch = null, Action<IEndpointConventionBuilder> endpoint = null)
            where T : class, ICommand
        {
            _builder.Post<T>(path, (cmd, ctx) => BuildCommandContext(cmd, ctx, beforeDispatch, afterDispatch),
                endpoint);

            return this;
        }

        public IDispatcherEndpointsBuilder Put(string path, Func<HttpContext, Task> context = null,
            Action<IEndpointConventionBuilder> endpoint = null)
        {
            _builder.Put(path, context, endpoint);

            return this;
        }

        public IDispatcherEndpointsBuilder Put<T>(string path, Func<T, HttpContext, Task> beforeDispatch = null,
            Func<T, HttpContext, Task> afterDispatch = null, Action<IEndpointConventionBuilder> endpoint = null)
            where T : class, ICommand
        {
            _builder.Put<T>(path, (cmd, ctx) => BuildCommandContext(cmd, ctx, beforeDispatch, afterDispatch), endpoint);

            return this;
        }

        public IDispatcherEndpointsBuilder Delete(string path, Func<HttpContext, Task> context = null,
            Action<IEndpointConventionBuilder> endpoint = null)
        {
            _builder.Delete(path, context, endpoint);

            return this;
        }

        public IDispatcherEndpointsBuilder Delete<T>(string path, Func<T, HttpContext, Task> beforeDispatch = null,
            Func<T, HttpContext, Task> afterDispatch = null, Action<IEndpointConventionBuilder> endpoint = null)
            where T : class, ICommand
        {
            _builder.Delete<T>(path, (cmd, ctx) => BuildCommandContext(cmd, ctx, beforeDispatch, afterDispatch),
                endpoint);

            return this;
        }

        private static async Task BuildCommandContext<T>(T command, HttpContext context,
            Func<T, HttpContext, Task> beforeDispatch = null,
            Func<T, HttpContext, Task> afterDispatch = null) where T : class, ICommand
        {
            if (beforeDispatch is {}) await beforeDispatch(command, context);

            var dispatcher = context.RequestServices.GetRequiredService<ICommandDispatcher>();
            await dispatcher.SendAsync(command);
            if (afterDispatch is null)
            {
                context.Response.StatusCode = 200;
                return;
            }

            await afterDispatch(command, context);
        }
    }
}