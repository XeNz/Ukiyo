using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Open.Serialization.Json;
using Ukiyo.Infrastructure.CQRS.Commands;
using Ukiyo.Infrastructure.CQRS.Dispatchers;
using Ukiyo.Infrastructure.CQRS.Queries;
using Ukiyo.Infrastructure.Ioc.Builders;
using Ukiyo.Infrastructure.Ioc.Extensions;
using Ukiyo.Infrastructure.WebApi.CQRS.Builders;
using Ukiyo.Infrastructure.WebApi.CQRS.Middlewares;
using Ukiyo.Infrastructure.WebApi.Exceptions;
using Ukiyo.Infrastructure.WebApi.Formatters;
using Ukiyo.Infrastructure.WebApi.Requests;

namespace Ukiyo.Infrastructure.WebApi.CQRS
{
    public static class Extensions
    {
        public static IApplicationBuilder UseDispatcherEndpoints(this IApplicationBuilder app,
            Action<IDispatcherEndpointsBuilder> builder)
        {
            var definitions = app.ApplicationServices.GetService<WebApiEndpointDefinitions>();
            app.UseRouting();
            app.UseEndpoints(router => builder(new DispatcherEndpointsBuilder(
                new EndpointsBuilder(router, definitions))));

            return app;
        }

        public static IDispatcherEndpointsBuilder Dispatch(this IEndpointsBuilder endpoints,
            Func<IDispatcherEndpointsBuilder, IDispatcherEndpointsBuilder> builder)
            => builder(new DispatcherEndpointsBuilder(endpoints));

        public static IApplicationBuilder UsePublicContracts<T>(this IApplicationBuilder app,
            string endpoint = "/_contracts") => app.UsePublicContracts(endpoint, typeof(T));

        public static IApplicationBuilder UsePublicContracts(this IApplicationBuilder app,
            bool attributeRequired, string endpoint = "/_contracts")
            => app.UsePublicContracts(endpoint, null, attributeRequired);

        public static IApplicationBuilder UsePublicContracts(this IApplicationBuilder app,
            string endpoint = "/_contracts", Type attributeType = null, bool attributeRequired = true)
            => app.UseMiddleware<PublicContractsMiddleware>(string.IsNullOrWhiteSpace(endpoint) ? "/_contracts" :
                endpoint.StartsWith("/") ? endpoint : $"/{endpoint}", attributeType ?? typeof(PublicContractAttribute),
                attributeRequired);

        public static Task SendAsync<T>(this HttpContext context, T command) where T : class, ICommand
            => context.RequestServices.GetService<ICommandDispatcher>().SendAsync(command);

        public static Task<TResult> QueryAsync<TResult>(this HttpContext context, IQuery<TResult> query)
            => context.RequestServices.GetService<IQueryDispatcher>().QueryAsync(query);

        public static Task<TResult> QueryAsync<TQuery, TResult>(this HttpContext context, TQuery query)
            where TQuery : class, IQuery<TResult>
            => context.RequestServices.GetService<IQueryDispatcher>().QueryAsync<TQuery, TResult>(query);
    }
}