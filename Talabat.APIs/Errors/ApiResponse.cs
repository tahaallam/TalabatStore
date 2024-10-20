﻿namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiResponse(int statusCode , string? message =null)
        {
            StatusCode = statusCode;
            Message = message??GetDefaultMessageForStatusCode(statusCode) ;
        }
        private string? GetDefaultMessageForStatusCode( int statusCode )
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "YouAreNotAuthorized",
                404 => "Resource Not Fount",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}
