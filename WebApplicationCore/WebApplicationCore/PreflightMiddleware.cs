using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCore
{
    class PreflightMiddleware
    {
        private readonly RequestDelegate _next;

        public PreflightMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            return BeginInvoke(context);
        }

        private Task BeginInvoke(HttpContext context)
        {
            if (context.Request.Method == "OPTIONS")
            {
                if (!string.IsNullOrEmpty( context.Request.Headers["Origin"]) )
                    context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { (string)context.Request.Headers["Origin"] });
                context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "foo,Access2, Origin, X-Requested-With, Content-Type, Accept,authentication" });
                context.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET, POST, PUT, DELETE, OPTIONS" });
                context.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });


                context.Response.StatusCode = 200;
                return Task.CompletedTask; //context.Response.WriteAsync("OK");
            }          
            return _next.Invoke(context); //complete request pipline 
        }
    }

    public static class PreflightMiddlewareExtensions
    {
        public static IApplicationBuilder UsePreflight(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PreflightMiddleware>();
        }
    }
}
