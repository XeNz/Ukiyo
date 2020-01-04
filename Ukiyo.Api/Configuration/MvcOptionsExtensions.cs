using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Utf8Json.AspNetCoreMvcFormatter;
using Utf8Json.Resolvers;

namespace Ukiyo.Api.Configuration
{
    public static class MvcOptionsExtensions
    {
        public static void AddUtf8JsonFormatters(this MvcOptions options)
        {
            options.OutputFormatters.RemoveType(typeof(SystemTextJsonOutputFormatter));
            options.InputFormatters.RemoveType(typeof(SystemTextJsonInputFormatter));

            var resolver = CompositeResolver.Create(EnumResolver.Default, StandardResolver.AllowPrivateCamelCase);

            options.OutputFormatters.Add(new JsonOutputFormatter(resolver));
            options.InputFormatters.Add(new JsonInputFormatter(resolver));
        }
    }
}