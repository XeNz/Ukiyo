using Ukiyo.Infrastructure.DAL.Base;
using Ukiyo.Infrastructure.DAL.Identity;

namespace Ukiyo.Core.Entities
{
    public class Comment : BaseEntity, IAggregateRoot
    {
        public int? ParentCommentId { get; set; }
        public string Content { get; set; }
        public ApplicationUser User { get; set; }
    }
}