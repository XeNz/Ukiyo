using JsonDiffPatch;
using Newtonsoft.Json.Linq;

namespace Ukiyo.Infrastructure.ObjectDiff
{
    public interface IJsonDifferProvider
    {
        /// <summary>
        ///     Creates a patch document based on two objects represented in JTokens
        /// </summary>
        /// <param name="from">From JToken</param>
        /// <param name="to">To JToken</param>
        /// <param name="useIdPropertyToDetermineEquality">Whether or not to use the 'Id' property to check if objects are equal</param>
        /// <returns></returns>
        PatchDocument Diff(JToken @from, JToken to, bool useIdPropertyToDetermineEquality);

        /// <summary>
        ///     Creates a patch document based on two objects represented in JTokens
        /// </summary>
        /// <param name="from">From object</param>
        /// <param name="to">To object</param>
        /// <param name="useIdPropertyToDetermineEquality">Whether or not to use the 'Id' property to check if objects are equal</param>
        /// <returns></returns>
        PatchDocument Diff(object @from, object to, bool useIdPropertyToDetermineEquality);
        
        
    }
}