using AutoMapper;
using BlogAPI.Data.Repositories;
using BlogAPI.DTOs;
using BlogAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository repository;
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;

        public AuthorsController(IAuthorRepository repository, IPostRepository postRepository, IMapper mapper)
        {
            this.repository = repository;
            this.postRepository = postRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthor()
        {
            var authors = await repository.GetAsync();
            var authorsDto = mapper.Map<IEnumerable<AuthorOnlyResponseDto>>(authors);
            return Ok(authorsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await repository.GetAsync(id);
            var authorDto = mapper.Map<AuthorOnlyResponseDto>(author);
            return Ok(authorDto);
        }

        [HttpGet("{id:int}/posts")]
        public async Task<IActionResult> GetPostByAuthorId(int id)
        {
            var posts = await postRepository.GetPostByAuthorId(id);
            var postDto = mapper.Map<IEnumerable<PostResponseDTO>>(posts);

            return Ok(postDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(AddAuthorDto authorDto)
        {
            var newAuthor = mapper.Map<Author>(authorDto);
            await repository.AddASync(newAuthor);
            return CreatedAtAction(nameof(GetAuthor), new { id = newAuthor.Id}, null);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorDto authorDto)
        {
            var author = mapper.Map<Author>(authorDto);
            await repository.EditAsync(author);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await repository.DeleteAsync(id);
            return NoContent();
        }
    }
}