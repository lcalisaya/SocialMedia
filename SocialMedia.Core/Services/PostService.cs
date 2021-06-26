using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    //Son clases en las que se van a reflejar las reglas de negocio/validaciones
    public class PostService : IPostService
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<User> _userRepository;
        public PostService(IRepository<Post> postRepository, IRepository<User> userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _postRepository.GetAll();
        }

        public async Task<Post> GetPost(int postId)
        {
            return await _postRepository.GetById(postId);
        }

        public async Task AddPost(Post jsonPost)
        {
            var user = await _userRepository.GetById(jsonPost.UserId);
            if (user == null)
            {
              throw new Exception("User doesn't exist");
            }

            if (jsonPost.Description.Contains("Sexo"))
            {
              throw new Exception("Content not allowed");
            }

            await _postRepository.Add(jsonPost);
        }

        public async Task<bool> UpdatePost(Post post)
        {
            await _postRepository.Update(post);
            return true;
        }

        public async Task<bool> DeletePost(int postId)
        {
            await _postRepository.Delete(postId);
            return true;
        }
    }
}
