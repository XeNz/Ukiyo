﻿using Ukiyo.Infrastructure.DAL.Base;

namespace Ukiyo.Core.Entities
{
    public class Post : BaseEntity, IAggregateRoot
    {
        public string Description { get; set; }

        public string Code { get; set; }
        // public Language CodeLanguage { get; set; }
        // public ApplicationUser User { get; set; }
    }
}