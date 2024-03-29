﻿using MundoDeDisney.MundoDeDisney.Core.CustomEntities;

namespace MundoDeDisney.Response
{
    public class ApiResponse<T>
    {
        public ApiResponse(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
        public Metadata Meta { get; set; }
    }
}
