using BlogAPI.Models;

namespace BlogAPI.DTOs
{
    public record EditPostDTO(int Id, string Title, string Body, int AuthorId);
}