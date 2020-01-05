using System;
using System.Linq;
using System.Threading.Tasks;
using Ukiyo.Core;
using Ukiyo.Infrastructure.CQRS.Commands;
using Z.EntityFramework.Plus;

namespace Ukiyo.Api.CQRS.Commands.Posts
{
    public class DeletePostCommand : ICommand
    {
        public Guid PostId { get; set; }
    }

    public class DeletePostCommandHandler : ICommandHandler<DeletePostCommand>
    {
        private readonly AppDbContext _context;

        public DeletePostCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public Task HandleAsync(DeletePostCommand command)
        {
            return _context.Posts
                .Where(x => x.Id == command.PostId)
                .DeleteAsync();
        }
    }
}