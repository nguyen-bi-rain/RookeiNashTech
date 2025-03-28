using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;

namespace Day_3_ASP_.NET.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var schema = context.Request.Scheme;
            var host = context.Request.Host.ToString();
            var path =  context.Request.Path;
            var query = context.Request.QueryString.ToString();
            var method = context.Request.Method;
            if(string.IsNullOrEmpty(query.Trim()))
            {
                query = "The request does not have query string";
            }

            string requestBody = await ReadRequestBodyAsync(context.Request);
            StringBuilder logMessage = new StringBuilder();
            logMessage.AppendLine($"{Environment.NewLine}");
            logMessage.AppendLine("------------------------------------------");
            logMessage.AppendLine($"{DateTime.Now}");
            logMessage.AppendLine($"Schema : {schema}");
            logMessage.AppendLine($"Host : {host}");
            logMessage.AppendLine($"Path : {path}");
            logMessage.AppendLine($"method : {method}");
            logMessage.AppendLine($"Query : {query}");
            logMessage.AppendLine($"Request Body : {requestBody}");
            logMessage.AppendLine("------------------------------------------");

            await FileHelper.WriteLogAsync(logMessage.ToString());
            await _next(context);
        }
        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            if (request.ContentLength == null || request.ContentLength == 0)
            {
                return "The request does not have body";
            }

            request.EnableBuffering();
            using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            string body = await reader.ReadToEndAsync();
            request.Body.Position = 0; // Reset stream position
            return body.ToString();
        }
    }
}