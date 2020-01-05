using Utf8Json;

namespace Ukiyo.Infrastructure.WebApi.Formatters
{
    internal class JsonInputFormatter : Utf8Json.AspNetCoreMvcFormatter.JsonInputFormatter
    {
        public JsonInputFormatter()
        {
        }

        public JsonInputFormatter(IJsonFormatterResolver resolver) : base(resolver)
        {
        }
    }
}