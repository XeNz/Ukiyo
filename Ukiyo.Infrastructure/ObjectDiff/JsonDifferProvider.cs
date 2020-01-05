using JsonDiffPatch;
using Newtonsoft.Json.Linq;

namespace Ukiyo.Infrastructure.ObjectDiff
{
    public class JsonDifferProvider : IJsonDifferProvider
    {
        private readonly JsonDiffer _differ;

        public JsonDifferProvider()
        {
            _differ = new JsonDiffer();
        }

        public PatchDocument Diff(JToken from, JToken to, bool useIdPropertyToDetermineEquality)
        {
            return DiffInternal(from, to, useIdPropertyToDetermineEquality);
        }

        public PatchDocument Diff(object from, object to, bool useIdPropertyToDetermineEquality)
        {
            return DiffInternal(JToken.FromObject(from), JToken.FromObject(to), useIdPropertyToDetermineEquality);
        }

        private PatchDocument DiffInternal(JToken from, JToken to, bool useIdPropertyToDetermineEquality)
        {
            return _differ.Diff(from, to, useIdPropertyToDetermineEquality);
        }
    }
}