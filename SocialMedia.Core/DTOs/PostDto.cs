using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.DTOs
{
    //Objetos planos solo para transmitir la información
    public class PostDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
