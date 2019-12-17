using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ukiyo.Infrastructure.DAL.Options;
using Ukiyo.Infrastructure.Ioc.Builders;
using Ukiyo.Infrastructure.Ioc.Extensions;

namespace Ukiyo.Infrastructure.DAL.Extensions
{
    public static class AddUnitOfWorkExtensions
    {
        private const string SectionName = "uow";

        public static IUkiyoBuilder AddUnitOfWork(this IUkiyoBuilder builder, string configurationSectionName = SectionName)
        {
            var options = builder.GetOptions<UnitOfWorkOptions>(configurationSectionName);
            return builder.AddUnitOfWork(options);
        }

        public static IUkiyoBuilder AddUnitOfWork(this IUkiyoBuilder builder, UnitOfWorkOptions unitOfWorkOptions)
        {
            // #TODO: maybe try scrutor to get all db contexts in in runtime assemblies
            if (unitOfWorkOptions != null && unitOfWorkOptions.AddAllDbContexts)
            {
                builder.Services.AddUnitOfWork<AppDbContext, IdentityDbContext>();
            }

            return builder;
        }

        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            services.AddScoped<IRepositoryFactory, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork.IUnitOfWork<TContext>, UnitOfWork<TContext>>();
            return services;
        }

        public static IServiceCollection AddUnitOfWork<TContext1, TContext2>(this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
        {
            services.AddScoped<IUnitOfWork.IUnitOfWork<TContext1>, UnitOfWork<TContext1>>();
            services.AddScoped<IUnitOfWork.IUnitOfWork<TContext2>, UnitOfWork<TContext2>>();
            return services;
        }

        public static IServiceCollection AddUnitOfWork<TContext1, TContext2, TContext3>(
            this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
            where TContext3 : DbContext
        {
            services.AddScoped<IUnitOfWork.IUnitOfWork<TContext1>, UnitOfWork<TContext1>>();
            services.AddScoped<IUnitOfWork.IUnitOfWork<TContext2>, UnitOfWork<TContext2>>();
            services.AddScoped<IUnitOfWork.IUnitOfWork<TContext3>, UnitOfWork<TContext3>>();
            return services;
        }

        public static IServiceCollection AddUnitOfWork<TContext1, TContext2, TContext3, TContext4>(
            this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
            where TContext3 : DbContext
            where TContext4 : DbContext
        {
            services.AddScoped<IUnitOfWork.IUnitOfWork<TContext1>, UnitOfWork<TContext1>>();
            services.AddScoped<IUnitOfWork.IUnitOfWork<TContext2>, UnitOfWork<TContext2>>();
            services.AddScoped<IUnitOfWork.IUnitOfWork<TContext3>, UnitOfWork<TContext3>>();
            services.AddScoped<IUnitOfWork.IUnitOfWork<TContext4>, UnitOfWork<TContext4>>();
            return services;
        }
    }
}