﻿namespace WebApiLautaroIriazabal.Provider
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ApiResponse(bool success, string message, object data)
        {
            this.Success = success;
            this.Message = message;
            this.Data = data;
        }
    }

}
