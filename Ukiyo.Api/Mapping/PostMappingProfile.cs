using System.Collections.Generic;
using AutoMapper;
using Ukiyo.Api.CQRS.Commands.Posts;
using Ukiyo.Api.CQRS.Queries.Posts;
using Ukiyo.Api.Dtos;
using Ukiyo.Core.Entities;
using Ukiyo.Infrastructure.DAL.Identity;

namespace Ukiyo.Api.Mapping
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<Post, PostDto>()
                .ReverseMap();

            CreateMap<IEnumerable<Post>, PostCollectionDto>()
                .ReverseMap();

            CreateMap<Language, LanguageDto>()
                .ReverseMap();

            CreateMap<ApplicationUser, UserDto>()
                .ReverseMap();

            CreateMap<CreatePostCommand, Post>()
                .ReverseMap();

            CreateMap<UpdatePostCommand, Post>()
                .ReverseMap();
        }
    }
}