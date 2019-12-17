using JsonDiffPatch;
using Tavis;

namespace Ukiyo.Infrastructure.ObjectDiff
{
    public static class PatchDocumentExtensions
    {
        public static PatchDocument PrependOperationPaths(this PatchDocument patchDocument, string pathToAppend)
        {
            foreach (var operation in patchDocument.Operations)
            {
                var oldPath = operation.Path.ToString();
                operation.Path = new JsonPointer($"{pathToAppend}{oldPath}");
            }

            return patchDocument;
        }
    }
}