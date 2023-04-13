using AutoMapper;
using BlogAPI.DTOs;
using BlogAPI.Models;

namespace BlogAPI.AutoMapperProfiles
{
    public class PostProfiles : Profile
    {
        public PostProfiles()
        {
            CreateMap<AddPostDTO, Post>();
            CreateMap<EditPostDTO, Post>();
            CreateMap<Post, PostResponseDTO>();
        }
    }
}