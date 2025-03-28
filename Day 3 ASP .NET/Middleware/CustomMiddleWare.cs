using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day_3_ASP_.NET.Middleware
{
    public class CustomMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddleWare> _logger;
        public CustomMiddleWare(RequestDelegate next, ILogger<CustomMiddleWare> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var schema = context.Request.Scheme;
            var host = context.Request.Host;
            var path =  context.Request.Path;
            var query = context.Request.QueryString;
            var requestBody = context.Request.Body;
            
            // Custom logic before the next middleware
            Console.WriteLine("Custom Middleware: Before the next middleware");

            // Call the next middleware in the pipeline
            await _next(context);

            // Custom logic after the next middleware
            Console.WriteLine("Custom Middleware: After the next middleware");
            
        }
    }
}