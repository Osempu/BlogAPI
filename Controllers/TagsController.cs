using AutoMapper;
using BlogAPI.Data.Repositories;
using BlogAPI.DTOs;
using BlogAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/tags")]
    public class TagsController : ControllerBase
    {
        private readonly ITagRepository repository;
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;

        public TagsController(ITagRepository repository, IPostRepository postRepository ,IMapper mapper)
        {
            this.repository = repository;
            this.postRepository = postRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTag()
        {
            var tags = await repository.GetAsync();
            var tagsDto = mapper.Map<IEnumerable<TagOnlyResponseDto>>(tags);
            return Ok(tagsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTag(int id)
        {
            var tag = await repository.GetAsync(id);
            var tagDto = mapper.Map<TagOnlyResponseDto>(tag);
            return Ok(tagDto);
        }

        [HttpGet("{id:int}/posts")]
        public async Task<IActionResult> GetPostFromTagId(int id)
        {
            var posts = await postRepository.GetPostByTagId(id);
            var postsDto = mapper.Map<IEnumerable<PostResponseDTO>>(posts);
            return Ok(postsDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag(AddTagDto tagDto)
        {
            var newTag = mapper.Map<Tag>(tagDto);
            await repository.AddAsync(newTag);
            return CreatedAtAction(nameof(GetTag), new {id = newTag.Id }, null);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTag(int id, UpdateTagDto tagDto)
        {
            var tag = mapper.Map<Tag>(tagDto);
            await repository.EditAsync(id, tag);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            await repository.DeleteAsync(id);
            return NoContent();
        }
    }
}