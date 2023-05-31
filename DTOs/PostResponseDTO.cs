using System.Text.Json.Serialization;
using BlogAPI.Models;

namespace BlogAPI.DTOs
{
    public record PostResponseDTO(
                [property: JsonPropertyOrder(1)] int Id,
                [property: JsonPropertyOrder(2)] string Title,
                [property: JsonPropertyOrder(3)] string Body,
                [property: JsonPropertyOrder(4)] DateTime CreatedDate,
                [property: JsonPropertyOrder(5)] AuthorOnlyResponseDto Author,
                [property: JsonPropertyOrder(6)] ICollection<TagOnlyResponseDto> Tags);
}