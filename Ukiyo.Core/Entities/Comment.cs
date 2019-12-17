using Ukiyo.Infrastructure.DAL.Base;

namespace Ukiyo.Core.Entities
{
    public class Comment : BaseEntity
    {
        public int? ParentCommentId { get; set; }
        public string Content { get; set; }
    }
}