using BlogAPI.Data;
using BlogAPI.Data.Repositories;
using BlogAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/post")]
    public class PostsController : ControllerBase
    {

        private readonly ILogger<PostsController> logger;
        private readonly IPostRepository repository;

        public PostsController(IPostRepository repository, ILogger<PostsController> logger)
        {
            this.logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetPost()
        {
            var posts = repository.GetPost();
            logger.LogDebug($"Get method called, got {posts.Count()} results");
            return Ok(posts);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetPost(int id)
        {
            try
            {
                var post = repository.GetPost(id);
                return Ok(post);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error getting post with id {id}");
                throw;
            }
        }

        [HttpPost]
        public IActionResult CreatePost(Post post)
        {
            try
            {
                repository.Add(post);
                return CreatedAtAction(nameof(GetPost), new { id = post.Id }, null);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error on Post method");
                throw;
            }
        }

        [HttpPut]
        public IActionResult EditPost([FromBody] Post post)
        {
            repository.Edit(post);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletePost(int id)
        {
            try
            {
                repository.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Unexpected error on Delete method trying to delete post with Id {id}");
                throw;
            }
        }
    }
}