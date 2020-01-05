using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Ukiyo.Api.Dtos;
using Ukiyo.Core;
using Ukiyo.Infrastructure.CQRS.Queries;

namespace Ukiyo.Api.CQRS.Queries
{
    public class GetPostByIdQuery : IQuery<PostDto>
    {
        public Guid PostId { get; set; }
    }

    public class GetPostByIdQueryHandler : IQueryHandler<GetPostByIdQuery, PostDto>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GetPostByIdQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<PostDto> HandleAsync(GetPostByIdQuery query)
        {
            return _context.Posts
                .Where(x => x.Id == query.PostId)
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }
    }
}