using JsonDiffPatch;
using Newtonsoft.Json.Linq;

namespace Ukiyo.Infrastructure.ObjectDiff
{
    public class JsonDiffer : IJsonDiffer
    {
        public PatchDocument Diff(JToken @from, JToken to, bool useIdPropertyToDetermineEquality)
        {
            var differ = new JsonDiffPatch.JsonDiffer();
            return differ.Diff(from, to, useIdPropertyToDetermineEquality);
        }
    }
}