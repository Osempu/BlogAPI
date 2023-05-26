using System.Net.Mime;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Data.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogDbContext context;

        public TagRepository(BlogDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Tag tag)
        {
            context.Add(tag);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tag = context.Tags.Find(id);
            context.Remove(tag);
            await context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, Tag tag)
        {
            context.Entry(tag).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tag>> GetAsync()
        {
            return await context.Tags.ToListAsync();
        }

        public async Task<Tag> GetAsync(int id)
        {
            return await context.Tags.FindAsync(id);
        }
    }
}