using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogDbContext context;
        public PostRepository(BlogDbContext context)
        {
            this.context = context;
        }

        public void Add(Post post)
        {
            context.Add(post);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var post = context.Posts.Find(id);
            context.Remove(post);
            context.SaveChanges();
        }

        public void Edit(Post post)
        {
            context.Entry(post).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Post> GetPost()
        {
            var allPosts = context.Posts.ToList();
            return allPosts;
        }

        public Post GetPost(int id)
        {
            var post = context.Posts.Find(id);
            return post;
        }
    }
}