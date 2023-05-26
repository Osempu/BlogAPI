using BlogAPI.Models;

namespace BlogAPI.DTOs
{
    public record PostResponseDTO(int Id, Author Author, string Title, string Body, DateTime CreatedDate);
}