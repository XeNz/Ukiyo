using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Ukiyo.Api.Dtos;
using Ukiyo.Infrastructure.CQRS.Queries;
using Ukiyo.Infrastructure.DAL;

namespace Ukiyo.Api.CQRS
{
    public class GetPostsQuery : IQuery<PostCollectionDto>
    {
    }

    public class PostCollectionDto
    {
        public IEnumerable<PostDto> Posts { get; set; }
    }

    public class GetPostsQueryHandler : IQueryHandler<GetPostsQuery, PostCollectionDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPostsQueryHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PostCollectionDto> HandleAsync(GetPostsQuery query)
        {
            var result = await _dbContext.Posts.ProjectTo<PostDto>(_mapper.ConfigurationProvider).ToListAsync();
            return new PostCollectionDto {Posts = result};
        }
    }
}