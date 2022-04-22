using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Demo
{
    public class CustomMillddleware
    {
        private readonly RequestDelegate next;
        public CustomMillddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext) {

            await httpContext.Response.WriteAsync("Response from Custome Middleware\n");
            await this.next(httpContext);
        }
    }
}
