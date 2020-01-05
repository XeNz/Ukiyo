using System;
using System.Threading.Tasks;
using AutoMapper;
using Ukiyo.Api.Dtos;
using Ukiyo.Core;
using Ukiyo.Core.Entities;
using Ukiyo.Infrastructure.CQRS.Commands;

namespace Ukiyo.Api.CQRS.Commands.Posts
{
    public class CreatePostCommand : ICommand
    {
        public CreatePostCommand(Guid id, string description, string code, LanguageDto codeLanguage)
        {
            Id = id == default ? Guid.NewGuid() : id;
            Description = description;
            Code = code;
            CodeLanguage = codeLanguage;
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public LanguageDto CodeLanguage { get; set; }
        public UserDto User { get; set; }
    }

    public class CreatePostHandler : ICommandHandler<CreatePostCommand>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CreatePostHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task HandleAsync(CreatePostCommand command)
        {
            // TODO get user from token
            var post = _mapper.Map<CreatePostCommand, Post>(command);
            await _context.AddAsync(post);
            await _context.SaveChangesAsync();
        }
    }
}