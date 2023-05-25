using BlogAPI.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogAPI.Filters
{
    public class PostExistsFilter : IAsyncActionFilter
    {
        private readonly IPostRepository postRepository;

        public PostExistsFilter(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionArguments.ContainsKey("id"))
            {
                context.Result = new BadRequestResult();
                return;
            }

            if (!(context.ActionArguments["id"] is int id))
            {
                context.Result = new BadRequestResult();
                return;
            }

            var post = await postRepository.GetPostAsync(id);

            if (post is null)
            {
                context.Result = new NotFoundObjectResult($"Post with id {id} does not exist.");
                return;
            }

            await next();
        }
    }
}