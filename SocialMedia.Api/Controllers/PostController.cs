using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;
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
            //Retorna un status 200
            return Ok(posts);
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPost(int postId)
        {
            var posts = await _postRepository.GetPost(postId);
            return Ok(posts);
        }

        // En este método se espera un objeto entidad que es el que se comunica con la BBDD.
        // Esto puede generar "Overposting", es decir que el usuario puede enviar más datos/objetos de los que son necesarios.
        // Ejemplo, se podría mandar a guardar un post + un comentario + un usuario 
        [HttpPost]
        public async Task<IActionResult> AddPost(Post jsonPost)
        {
            await _postRepository.AddPost(jsonPost);
            return Ok(jsonPost);
        }

  }
}
