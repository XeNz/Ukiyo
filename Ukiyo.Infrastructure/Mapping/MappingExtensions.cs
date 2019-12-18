using System;
using AutoMapper;
using Ukiyo.Infrastructure.Ioc.Builders;

namespace Ukiyo.Infrastructure.Mapping
{
    public static class MappingExtensions
    {
        public static IUkiyoBuilder AddAutoMapper(this IUkiyoBuilder builder, Type type)
        {
            builder.Services.AddAutoMapper(x =>
            {
                x.AddMaps(type);
                x.AllowNullCollections = true;
            }, type);

            return builder;
        }
    }
}