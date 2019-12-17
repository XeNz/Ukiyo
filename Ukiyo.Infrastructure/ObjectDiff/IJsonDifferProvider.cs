using JsonDiffPatch;
using Newtonsoft.Json.Linq;

namespace Ukiyo.Infrastructure.ObjectDiff
{
    public interface IJsonDiffer
    {
        PatchDocument Diff(JToken @from, JToken to, bool useIdPropertyToDetermineEquality);
    }
}