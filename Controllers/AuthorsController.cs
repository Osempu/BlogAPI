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
        private readonly IMapper mapper;

        public AuthorsController(IAuthorRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthor()
        {
            var authors = await repository.GetAsync();
            return Ok(authors);
        }

        [HttpGet("id:int")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await repository.GetAsync(id);
            return Ok(author);
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