using System.Text.Json.Serialization;
using BlogAPI.DTOs;
using RiskFirst.Hateoas.Models;

namespace BlogAPI.Models
{
    public class PostHateoasResponse : ILinkContainer
    {
        public PostResponseDTO Data;
        private Dictionary<string, Link> links;

        [JsonPropertyName("links")]
        public Dictionary<string, Link> Links 
        { 
            get => links ?? (links = new Dictionary<string, Link>());
            set => links = value;
        }

        public void AddLink(string id, Link link)
        {
            Links.Add(id, link);
        }
    }
}