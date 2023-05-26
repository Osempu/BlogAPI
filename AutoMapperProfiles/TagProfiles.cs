using AutoMapper;
using BlogAPI.DTOs;
using BlogAPI.Models;

namespace BlogAPI.AutoMapperProfiles
{
    public class TagProfiles : Profile
    {
        public TagProfiles()
        {
            CreateMap<AddTagDto, Tag>();
            CreateMap<UpdateTagDto, Tag>();
        }
    }
}