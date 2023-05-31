using AutoMapper;
using BlogAPI.DTOs;
using BlogAPI.Models;

namespace BlogAPI.AutoMapperProfiles
{
    public class AuthorProfiles : Profile
    {
        public AuthorProfiles()
        {
            CreateMap<AddAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();

            CreateMap<Author, AuthorResponseDto>();
            CreateMap<Author, AuthorOnlyResponseDto>();
        }
    }
}