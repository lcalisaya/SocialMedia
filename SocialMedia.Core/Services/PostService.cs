﻿using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Post> GetPosts()
        {
            return _unitOfWork.PostRepository.GetAll();
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
                throw new BusinessException("User doesn't exist");
            }

            if (jsonPost.Description.Contains("Sexo"))
            {
                throw new BusinessException("Content not allowed");
            }
            
            var userPosts = await _unitOfWork.PostRepository.GetPostsByUser(jsonPost.UserId);
            if (userPosts.Count() != 0 && userPosts.Count() < 10)
            { 
                var lastPost = userPosts.OrderByDescending(x => x.Date).FirstOrDefault();
                if ((DateTime.Now - lastPost.Date).TotalDays < 7)
                { 
                    throw new BusinessException("You are not able to publish the post");
                }
            }

            await _unitOfWork.PostRepository.Add(jsonPost);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePost(int postId)
        {
            await _unitOfWork.PostRepository.Delete(postId);
            return true;
        }
    }
}
