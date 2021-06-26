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
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task AddPost(Post jsonPost)
        {
            var user = await _userRepository.GetUser(jsonPost.UserId);
            if (user == null)
            {   
                throw new Exception("User doesn't exist");
            }

            if (jsonPost.Description.Contains("Sexo"))
            {   
                throw new Exception("Content not allowed");
            }
            
            await _postRepository.AddPost(jsonPost);
        }

        public async Task<bool> DeletePost(int postId)
        {
            return await _postRepository.DeletePost(postId);
        }

        public async Task<Post> GetPost(int postId)
        {
            return await _postRepository.GetPost(postId);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _postRepository.GetPosts();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            return await _postRepository.UpdatePost(post);
        }
    }
}
