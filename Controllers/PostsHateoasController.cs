using AutoMapper;
using BlogAPI.Data.Repositories;
using BlogAPI.DTOs;
using BlogAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RiskFirst.Hateoas;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/hateoas/posts")]
    public class PostsHateoasController : ControllerBase
    {
        private readonly ILinksService linksService;
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;

        public PostsHateoasController(ILinksService linksService, IPostRepository postRepository, IMapper mapper)
        {
            this.linksService = linksService;
            this.postRepository = postRepository;
            this.mapper = mapper;
        }

        [HttpGet(Name = nameof(Get))]
        public async Task<IActionResult> Get([FromQuery] QueryParameters parameters)
        {
            var posts = await postRepository.GetPostAsync(parameters);
            var postsDto = mapper.Map<IEnumerable<PostResponseDTO>>(posts);

            var hateoasResults = new List<PostHateoasResponse>();

            foreach (var post in postsDto)
            {
                var hateoasResult = new PostHateoasResponse { Data = post};
                await linksService.AddLinksAsync(hateoasResult);
                hateoasResults.Add(hateoasResult);
            }

            return Ok(hateoasResults);
        }

        [HttpGet("{id:int}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            var post = await postRepository.GetPostAsync(id);
            var postDto = mapper.Map<PostResponseDTO>(post);
            var hateoasResult = new PostHateoasResponse{Data = postDto};
            await linksService.AddLinksAsync(hateoasResult);

            return Ok(hateoasResult);
        }

        [HttpPost(Name = nameof(Post))]
        public async Task<IActionResult> Post(AddPostDTO addPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newPost = mapper.Map<AddPostDTO, Post>(addPostDTO);
            newPost.CreatedDate = DateTime.Now;
            await postRepository.AddAsync(newPost);
            return CreatedAtAction(nameof(Get), new { id = newPost.Id }, null);
        }

        [HttpPut("{id:int}", Name = nameof(Edit))]
        public async Task<IActionResult> Edit(int id, [FromBody] EditPostDTO editPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var post = mapper.Map<EditPostDTO, Post>(editPostDto);

            post.LastUpdated = DateTime.Now;
            await postRepository.EditAsync(post);
            return NoContent();
        }

        [HttpDelete("{id:int}", Name = nameof(Delete))]
        public async Task<IActionResult> Delete(int id)
        {
            await postRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = nameof(Patch))]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<Post> doc)
        {
            var post = await postRepository.GetPostAsync(id);

            doc.ApplyTo(post);
            await postRepository.EditAsync(post);

            return NoContent();
        }
    }
}