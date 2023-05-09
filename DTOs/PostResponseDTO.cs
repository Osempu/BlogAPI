namespace BlogAPI.DTOs
{
    public record PostResponseDTO(int Id, string Author, string Title, string Body, DateTime CreatedDate);
}