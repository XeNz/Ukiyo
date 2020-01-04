using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ukiyo.Api.Dtos;
using Ukiyo.Core;
using Ukiyo.Infrastructure.CQRS.Queries;

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
            // var result = await _dbContext.Posts.ProjectTo<PostDto>(_mapper.ConfigurationProvider).ToListAsync();
            var result = _mapper.Map<IEnumerable<PostDto>>(await _dbContext.Posts.ToListAsync());
            return new PostCollectionDto {Posts = result};
        }
    }
}