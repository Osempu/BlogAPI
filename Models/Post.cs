namespace BlogAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}