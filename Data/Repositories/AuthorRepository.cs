using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BlogDbContext context;

        public AuthorRepository(BlogDbContext context)
        {
            this.context = context;
        }

        public async Task AddASync(Author author)
        {
            context.Add(author);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var author = context.Authors.Find(id);
            context.Remove(author);
            await context.SaveChangesAsync();
        }

        public async Task EditAsync(Author author)
        {
            context.Entry(author).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAsync()
        {
            return await context.Authors.ToListAsync();
        }

        public async Task<Author> GetAsync(int id)
        {
            var author = await context.Authors.AsNoTracking()
                                                .Where(x => x.Id == id)
                                                .FirstOrDefaultAsync();
            return author;
        }
    }
}