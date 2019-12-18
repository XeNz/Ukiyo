using System;

namespace Ukiyo.Api.Dtos
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public LanguageDto CodeLanguage { get; set; }
        public UserDto User { get; set; }
    }
}