using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Ukiyo.Infrastructure.CQRS.Commands;
using Ukiyo.Infrastructure.CQRS.Queries;

namespace Ukiyo.Infrastructure.WebApi.CQRS
{
    public interface IDispatcherEndpointsBuilder
    {
        IDispatcherEndpointsBuilder Get(string path, Func<HttpContext, Task> context = null,
            Action<IEndpointConventionBuilder> endpoint = null);

        IDispatcherEndpointsBuilder Get<TQuery, TResult>(string path,
            Func<TQuery, HttpContext, Task> beforeDispatch = null,
            Func<TQuery, TResult, HttpContext, Task> afterDispatch = null,
            Action<IEndpointConventionBuilder> endpoint = null) where TQuery : class, IQuery<TResult>;

        IDispatcherEndpointsBuilder Post(string path, Func<HttpContext, Task> context = null,
            Action<IEndpointConventionBuilder> endpoint = null);

        IDispatcherEndpointsBuilder Post<T>(string path, Func<T, HttpContext, Task> beforeDispatch = null,
            Func<T, HttpContext, Task> afterDispatch = null, Action<IEndpointConventionBuilder> endpoint = null)
            where T : class, ICommand;

        IDispatcherEndpointsBuilder Put(string path, Func<HttpContext, Task> context = null,
            Action<IEndpointConventionBuilder> endpoint = null);

        IDispatcherEndpointsBuilder Put<T>(string path, Func<T, HttpContext, Task> beforeDispatch = null,
            Func<T, HttpContext, Task> afterDispatch = null, Action<IEndpointConventionBuilder> endpoint = null)
            where T : class, ICommand;

        IDispatcherEndpointsBuilder Delete(string path, Func<HttpContext, Task> context = null,
            Action<IEndpointConventionBuilder> endpoint = null);

        IDispatcherEndpointsBuilder Delete<T>(string path, Func<T, HttpContext, Task> beforeDispatch = null,
            Func<T, HttpContext, Task> afterDispatch = null, Action<IEndpointConventionBuilder> endpoint = null)
            where T : class, ICommand;
    }
}