using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Ukiyo.Infrastructure.WebApi.Formatters
{
    internal class JsonInputFormatter : Utf8Json.AspNetCoreMvcFormatter.JsonInputFormatter
    {
        // private const string EmptyJson = "{}";
        // private readonly ConcurrentDictionary<Type, MethodInfo> _methods = new ConcurrentDictionary<Type, MethodInfo>();
        // private readonly IJsonSerializer _serializer;
        // private readonly MethodInfo _deserializeMethod;
        //
        // public JsonInputFormatter(IJsonSerializer serializer)
        // {
        //     _serializer = serializer;
        //     _deserializeMethod = _serializer.GetType().GetMethod(nameof(_serializer.Deserialize));
        // }
        //
        // public bool CanRead(InputFormatterContext context)
        // {
        //     return true;
        // }
        //
        // public async Task<InputFormatterResult> ReadAsync(InputFormatterContext context)
        // {
        //     if (!_methods.TryGetValue(context.ModelType, out var method))
        //     {
        //         method = _deserializeMethod.MakeGenericMethod(context.ModelType);
        //         _methods.TryAdd(context.ModelType, method);
        //     }
        //
        //     var request = context.HttpContext.Request;
        //     var json = string.Empty;
        //     if (!(request.Body is null && request.Body.Length > 0))
        //     {
        //         using var streamReader = new StreamReader(request.Body);
        //         json = await streamReader.ReadToEndAsync();
        //     }
        //
        //     if (string.IsNullOrWhiteSpace(json))
        //     {
        //         json = EmptyJson;
        //     }
        //
        //     var result = method.Invoke(_serializer, new object[] {json});
        //
        //     return await InputFormatterResult.SuccessAsync(result);
        // }
        
        
    }
}