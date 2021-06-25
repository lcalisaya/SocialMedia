﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Responses
{ 
    //Para manejar una estructura general en todas las respuestas de las operaciones CRUD
    public class ApiResponse<T>
    {
        public T Data { get; set; }

        public ApiResponse(T data)
        {
            Data = data;
        }
    }
}