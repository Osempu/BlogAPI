using BlogAPI.Models;

namespace BlogAPI.Data.Repositories
{
    public interface IPostRepository 
    {
        IEnumerable<Post> GetPost();
        Post GetPost(int id);
        void Add(Post post);
        void Edit(Post post);
        void Delete(int id);
    }
}