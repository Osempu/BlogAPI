using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BlogAPI.Data;
using BlogAPI.Data.Repositories;
using BlogAPI.DTOs;
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
        private readonly IMapper mapper;

        public PostsController(IPostRepository repository, ILogger<PostsController> logger,
         IMapper mapper)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetPost()
        {
            var posts = repository.GetPost();
            var postsDto = mapper.Map<IEnumerable<PostResponseDTO>>(posts);
            logger.LogDebug($"Get method called, got {postsDto.Count()} results");
            return Ok(postsDto);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetPost(int id)
        {
            try
            {
                var post = repository.GetPost(id);
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
        public IActionResult CreatePost(AddPostDTO addPostDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var newPost = mapper.Map<AddPostDTO, Post>(addPostDTO);
                newPost.CreatedDate = DateTime.Now;
                repository.Add(newPost);
                return CreatedAtAction(nameof(GetPost), new { id = newPost.Id }, null);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error on Post method");
                throw;
            }
        }

        [HttpPut]
        public IActionResult EditPost([FromBody] EditPostDTO editPostDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var post = mapper.Map<EditPostDTO, Post>(editPostDto);

                post.LastUpdated = DateTime.Now;
                repository.Edit(post);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error on Put(Edit) Method");
                throw;
            }
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