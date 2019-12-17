using Ukiyo.Infrastructure.DAL.Base;

namespace Ukiyo.Core.Entities
{
    public class Language : BaseEntity
    {
        public string Abbrevition { get; set; }
        public string Name { get; set; }
    }
}