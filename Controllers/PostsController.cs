using BlogAPI.Data;
using BlogAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/post")]
    public class PostsController : ControllerBase
    {
        private readonly BlogDbContext context;
        private readonly ILogger<PostsController> logger;

        public PostsController(BlogDbContext context, ILogger<PostsController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult GetPost()
        {
            var posts = context.Posts.ToList();
            logger.LogDebug($"Get method called, got {posts.Count()} results");
            return Ok(posts);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetPost(int id)
        {
            try
            {
                var post = context.Posts.Find(id);
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
                context.Add(post);
                context.SaveChanges();

                return CreatedAtAction(nameof(GetPost), new { id = post.Id }, null);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error on Post method");
                throw;
            }
        }

        [HttpPut]
        public IActionResult EditPost(int id, [FromBody] Post post)
        {
            var postToEdit = context.Posts.Find(id);

            if (postToEdit is null)
            {
                logger.LogError($"Post with id {id} was not found");
                throw new NullReferenceException("Post with id {id} was not found");
            }

            postToEdit.Title = post.Title;
            postToEdit.Body = post.Body;
            postToEdit.Author = post.Author;

            context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletePost(int id)
        {
            try
            {
                var post = context.Posts.Find(id);
                context.Posts.Remove(post);
                context.SaveChanges();

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