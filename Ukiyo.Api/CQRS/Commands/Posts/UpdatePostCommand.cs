using System;
using System.Threading.Tasks;
using AutoMapper;
using Ukiyo.Api.Dtos;
using Ukiyo.Core;
using Ukiyo.Core.Entities;
using Ukiyo.Infrastructure.CQRS.Commands;

namespace Ukiyo.Api.CQRS.Commands.Posts
{
    public class UpdatePostCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public LanguageDto CodeLanguage { get; set; }
    }
    
    public class UpdatePostCommandHandler : ICommandHandler<UpdatePostCommand>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdatePostCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task HandleAsync(UpdatePostCommand command)
        {
            var mapped = _mapper.Map<UpdatePostCommand, Post>(command);
            _appDbContext.Update(mapped);
            await _appDbContext.SaveChangesAsync();
        }
    }
}