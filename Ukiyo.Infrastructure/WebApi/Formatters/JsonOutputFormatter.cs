using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Utf8Json;

namespace Ukiyo.Infrastructure.WebApi.Formatters
{
    internal class JsonOutputFormatter : Utf8Json.AspNetCoreMvcFormatter.JsonOutputFormatter
    {
    }
}