using BlogAPI.Models;

namespace BlogAPI.DTOs
{
    public record AuthorResponseDto(int Id, string Name, ICollection<Post> Posts);
}