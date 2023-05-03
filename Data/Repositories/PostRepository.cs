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

        public async Task AddAsync(Post post)
        {
            context.Add(post);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var post = context.Posts.Find(id);
            context.Remove(post);
            await context.SaveChangesAsync();
        }

        public async Task EditAsync(Post post)
        {
            context.Entry(post).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetPostAsync(QueryParameters parameters)
        {
            var allPosts = context.Posts.AsQueryable();

            //Filter by Author
            if(!string.IsNullOrEmpty(parameters.Author))
            {
                allPosts = allPosts.Where(x => x.Author == parameters.Author);
            }

            var pagedPosts = await allPosts
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToListAsync();

            return pagedPosts;
        }

        public async Task<Post> GetPostAsync(int id)
        {
            var post = await context.Posts.FindAsync(id);
            return post;
        }
    }
}