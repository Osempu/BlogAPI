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

        public PostsController(BlogDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetPost()
        {
            var posts = context.Posts.ToList();
            return Ok(posts);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetPost(int id)
        {
            var post = context.Posts.Find(id);
            return Ok(post);
        }

        [HttpPost]
        public IActionResult CreatePost(Post post)
        {
            context.Add(post);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetPost), new {id = post.Id}, null);
        }

        [HttpPut]
        public IActionResult EditPost(int id, [FromBody]Post post)
        {
            var postToEdit = context.Posts.Find(id);
            postToEdit.Title = post.Title;
            postToEdit.Body = post.Body;
            postToEdit.Author = post.Author;

            context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletePost(int id)
        {
            var post = context.Posts.Find(id);
            context.Posts.Remove(post);
            context.SaveChanges();

            return NoContent();
        }
    }
}