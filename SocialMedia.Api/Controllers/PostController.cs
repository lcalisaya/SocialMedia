using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // Este decorador activa las validaciones a los modelos
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        //Inyectar vía constructor: Se le pasan los objetos que de este dependan
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts() 
        {
            //Bajo Acoplamiento y Alta cohesión: que las clases no dependan entre sí
            //Solución:Inyección de dependencias, trabajar con abstracciones interfaces
            var posts = await _postRepository.GetPosts();
            
            //Se convierte la respuesta en objetos DTO para que el usuario no tenga contacto con nuestra entidad de dominio
            var postsDto = posts.Select(x => new PostDto 
            { 
                PostId = x.PostId,
                UserId = x.UserId,
                Description = x.Description,
                Image = x.Image,
                Date = x.Date
            });

            //Retorna un status 200
            return Ok(postsDto);
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPost(int postId)
        {
            var post = await _postRepository.GetPost(postId);
            var postDto = new PostDto 
            { 
                PostId = post.PostId,
                UserId = post.UserId,
                Description = post.Description,
                Image = post.Image,
                Date = post.Date
            };
            return Ok(postDto);
        }

        // En este método se espera un objeto entidad que es el que se comunica con la BBDD.
        // Esto puede generar "Overposting", es decir que el usuario puede enviar más datos/objetos de los que son necesarios.
        // Ejemplo, se podría mandar a guardar un post + un comentario + un usuario 
        [HttpPost]
        public async Task<IActionResult> AddPost(PostDto jsonPost)
        {
            var post = new Post 
            { 
                UserId = jsonPost.UserId,
                Description = jsonPost.Description,
                Image = jsonPost.Image,
                Date = jsonPost.Date
            };
            await _postRepository.AddPost(post);
            return Ok(post);
        }

  }
}
