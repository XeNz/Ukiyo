using System;

namespace Ukiyo.Infrastructure.DAL
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}