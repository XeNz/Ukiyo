using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ukiyo.Api.CQRS;
using Ukiyo.Infrastructure.CQRS.Dispatchers;

namespace Ukiyo.Api.Controllers
{
    // [Route("posts")]
    // public class PostsController : Controller
    // {
    //     private readonly IQueryDispatcher _queryDispatcher;
    //     private readonly ICommandDispatcher _commandDispatcher;
    //
    //     public PostsController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    //     {
    //         _queryDispatcher = queryDispatcher;
    //         _commandDispatcher = commandDispatcher;
    //     }
    //
    //     [HttpGet]
    //     public async Task<ActionResult> GetPost()
    //     {
    //         var result = await _queryDispatcher.QueryAsync<GetPostsQuery, PostCollectionDto>(new GetPostsQuery());
    //         return Ok(result);
    //     }
    //     
    //     [HttpPost]
    //     public async Task<ActionResult> CreatePost()
    //     {
    //         var result = await _commandDispatcher.SendAsync<CreatePostCommand, PostCollectionDto>(new GetPostsQuery());
    //         return Ok(result);
    //     }
    // }
}