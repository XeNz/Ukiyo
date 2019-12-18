using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ukiyo.Api.CQRS;
using Ukiyo.Infrastructure.CQRS.Dispatchers;

namespace Ukiyo.Api.Controllers
{
    [Route("posts")]
    public class PostsController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public PostsController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult> GetPost()
        {
            var result = await _queryDispatcher.QueryAsync<GetPostsQuery, PostCollectionDto>(new GetPostsQuery());
            return Ok(result);
        }
    }
}