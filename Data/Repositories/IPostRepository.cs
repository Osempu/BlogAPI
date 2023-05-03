using BlogAPI.Models;

namespace BlogAPI.Data.Repositories
{
    public interface IPostRepository 
    {
        Task<IEnumerable<Post>> GetPostAsync(QueryParameters parameters);
        Task<Post> GetPostAsync(int id);
        Task AddAsync(Post post);
        Task EditAsync(Post post);
        Task DeleteAsync(int id);
    }
}