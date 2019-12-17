using Ukiyo.Infrastructure.DAL.Base;

namespace Ukiyo.Core.Entities
{
    public class Language : BaseEntity
    {
        public string Abbreviation { get; set; }
        public string Name { get; set; }
    }
}