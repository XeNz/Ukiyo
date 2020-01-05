using Utf8Json;

namespace Ukiyo.Infrastructure.WebApi.Formatters
{
    internal class JsonOutputFormatter : Utf8Json.AspNetCoreMvcFormatter.JsonOutputFormatter
    {
        public JsonOutputFormatter()
        {
        }

        public JsonOutputFormatter(IJsonFormatterResolver resolver) : base(resolver)
        {
        }
    }
}