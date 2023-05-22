using AutoMapper;
using BlogAPI.DTOs;
using BlogAPI.Models;
using Microsoft.AspNetCore.Mvc;
using BlogAPI.Data.Repositories;
using Microsoft.AspNetCore.JsonPatch;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/post")]
    public class PostsController : ControllerBase
    {

        private readonly ILogger<PostsController> logger;
        private readonly IPostRepository repository;
        private readonly IMapper mapper;

        public PostsController(IPostRepository repository, ILogger<PostsController> logger,
         IMapper mapper)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPost([FromQuery] QueryParameters parameters)
        {
            var posts = await repository.GetPostAsync(parameters);

            var postsDto = mapper.Map<IEnumerable<PostResponseDTO>>(posts);

            logger.LogDebug($"Get method called, got {postsDto.Count()} results");
            return Ok(postsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPost(int id)
        {
            try
            {
                var post = await repository.GetPostAsync(id);
                var postDto = mapper.Map<PostResponseDTO>(post);

                return Ok(postDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error getting post with id {id}");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(AddPostDTO addPostDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var newPost = mapper.Map<AddPostDTO, Post>(addPostDTO);
                newPost.CreatedDate = DateTime.Now;
                await repository.AddAsync(newPost);
                return CreatedAtAction(nameof(GetPost), new { id = newPost.Id }, null);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error on Post method");
                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditPost([FromBody] EditPostDTO editPostDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var post = mapper.Map<EditPostDTO, Post>(editPostDto);

                post.LastUpdated = DateTime.Now;
                await repository.EditAsync(post);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error on Put(Edit) Method");
                throw;
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                await repository.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Unexpected error on Delete method trying to delete post with Id {id}");
                throw;
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> PatchPost(int id, [FromBody] JsonPatchDocument<Post> doc)
        {
            var post = await repository.GetPostAsync(id);

            if(post is null)
            {
                return NotFound();
            }

            doc.ApplyTo(post);
            await repository.EditAsync(post);

            return NoContent();
        }
    }
}