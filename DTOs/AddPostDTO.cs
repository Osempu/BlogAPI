using BlogAPI.Models;

namespace BlogAPI.DTOs
{
    public record AddPostDTO(string Title, string Body, int AuthorId);
}