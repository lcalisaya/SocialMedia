using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            //Se solicita que un objeto Post lo mapee/convierta a un objeto PostDto
            CreateMap<Post, PostDto>();
            
            //Se solicita que un objeto PostDto lo mapee/convierta a un objeto Post
            CreateMap<PostDto, Post>();
        }
    }
}
