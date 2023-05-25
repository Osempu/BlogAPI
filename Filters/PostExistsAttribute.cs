using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Filters
{
    public class PostExistsAttribute : TypeFilterAttribute
    {
        public PostExistsAttribute() : base(typeof(PostExistsFilter))
        {
        }
    }
}