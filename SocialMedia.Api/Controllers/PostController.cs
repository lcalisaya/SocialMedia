using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMedia.Api.Responses;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // Este decorador activa las validaciones a los modelos

    //La clase ControllerBase existe para trabajar con APIs
    //La clase Controller además de hacer lo mismo que ControllerBase, agrega funciones para trabajar en MVC
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        //Inyectar vía constructor: Se le pasan los objetos que de este dependan
        public PostController(IPostService postService, IMapper mapper, IUriService uriService)
        {
            _postService = postService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet(Name = nameof(GetPosts))]
        //Decoradores necesarios para especificar el tipo de respuesta en la documentación
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //Parámetros que se pasan por query string, en forma individual o en un objeto
        public IActionResult GetPosts([FromQuery]PostQueryFilter filters) 
        {
            //Bajo Acoplamiento y Alta cohesión: que las clases no dependan entre sí
            //Solución:Inyección de dependencias, trabajar con abstracciones interfaces
            var posts = _postService.GetPosts(filters);
            
            //Se convierte la respuesta en objetos DTO para que el usuario no tenga contacto con nuestra entidad de dominio
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            //En el header del response se enviará este objeto
            var metadata = new MetaData{ 
                PageSize = posts.PageSize,
                TotalCount = posts.TotalCount,
                TotalPages = posts.TotalPages,
                CurrentPage = posts.CurrentPage,
                HasPreviousPage = posts.HasPreviousPage,
                HasNextPage = posts.HasNextPage,
                NextPageUrl = _uriService.GetPostsPaginationUri(filters, Url.RouteUrl(nameof(GetPosts))).ToString(),
                PreviousPageUrl = _uriService.GetPostsPaginationUri(filters, Url.RouteUrl(nameof(GetPosts))).ToString()
            };

            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto) 
            {
                Meta = metadata
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            //Retorna un status 200
            return Ok(response);
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPost(int postId)
        {
            var post = await _postService.GetPost(postId);
            var postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        // En este método se espera un objeto entidad que es el que se comunica con la BBDD.
        // Esto puede generar "Overposting", es decir que el usuario puede enviar más datos/objetos de los que son necesarios.
        // Ejemplo, se podría mandar a guardar un post + un comentario + un usuario 
        [HttpPost]
        public async Task<IActionResult> AddPost(PostDto jsonPost)
        {
            var post = _mapper.Map<Post>(jsonPost);
            await _postService.AddPost(post);
            
            var postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(int idPost, PostDto jsonPost)
        {
            var post = _mapper.Map<Post>(jsonPost);
            post.Id = idPost;
            var result = await _postService.UpdatePost(post);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(int idPost)
        {
            var result = await _postService.DeletePost(idPost);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

    }
}
