﻿using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    //Son clases en las que se van a reflejar las reglas de negocio/validaciones
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _unitOfWork.PostRepository.GetAll();
        }

        public async Task<Post> GetPost(int postId)
        {
            return await _unitOfWork.PostRepository.GetById(postId);
        }

        public async Task AddPost(Post jsonPost)
        {
            var user = await _unitOfWork.UserRepository.GetById(jsonPost.UserId);
            if (user == null)
            {
              throw new Exception("User doesn't exist");
            }

            if (jsonPost.Description.Contains("Sexo"))
            {
              throw new Exception("Content not allowed");
            }

            await _unitOfWork.PostRepository.Add(jsonPost);
        }

        public async Task<bool> UpdatePost(Post post)
        {
            await _unitOfWork.PostRepository.Update(post);
            return true;
        }

        public async Task<bool> DeletePost(int postId)
        {
            await _unitOfWork.PostRepository.Delete(postId);
            return true;
        }
    }
}
