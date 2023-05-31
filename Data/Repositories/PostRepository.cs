using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

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
            var allPosts = context.Posts.AsNoTracking().AsQueryable();

            //Filter by Author
            if(!string.IsNullOrEmpty(parameters.Author))
            {
                allPosts = allPosts.Where(x => x.Author.Name == parameters.Author);
            }

            //Sort posts
            if(!string.IsNullOrEmpty(parameters.OrderBy))
            {
                string orderFlow = parameters.OrderAsc ? "ascending" : "descending";
                allPosts = allPosts.OrderBy($"{parameters.OrderBy} {orderFlow}");
            }

            //Paginated posts
            var pagedPosts = await allPosts
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .Include(x => x.Author)
                    .Include(x => x.Tags)
                    .ToListAsync();

            return pagedPosts;
        }

        public async Task<Post> GetPostAsync(int id)
        {
            var post = await context.Posts.AsNoTracking()
                                            .Where(x => x.Id == id)
                                            .Include(x => x.Author)
                                            .Include(x => x.Tags)
                                            .FirstOrDefaultAsync();
            return post;
        }

        public async Task<IEnumerable<Post>> GetPostByAuthorId(int id)
        {
            var posts = await context.Posts.AsNoTracking()
                                            .Where(x => x.AuthorId == id)
                                            .Include(x => x.Author)
                                            .Include(x => x.Tags)
                                            .ToListAsync();
            return posts;
        }

        public async Task<IEnumerable<Post>> GetPostByTagId(int id)
        {
            var posts = await context.Posts.AsNoTracking()
                                            .Include(x => x.Author)
                                            .Include(x => x.Tags)
                                            .Where(x => x.Tags.Any(t => t.Id == id))
                                            .ToListAsync();

            return posts;
        }
    }
}