using System;

namespace SocialMedia.Core.DTOs
{
    //Objetos planos solo para transmitir la información
    public class PostDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        //Con el signo '?' Date es nullable
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
