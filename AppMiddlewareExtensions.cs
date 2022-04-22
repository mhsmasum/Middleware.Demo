using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Demo
{
    public static class AppMiddlewareExtensions
    {
        public static void UseExtensions(this IApplicationBuilder app)
        {

            // ***The Run method is The termination point of a middle ware
            #region example with Run method
            // no milldeware will exexute after the Run statement,
            //app.Run( async context => {
            //    await context.Response.WriteAsync("Response of first middleware");

            //} );
            //app.Run(async context => {
            //    await context.Response.WriteAsync("Response of 2nd middleware");
            //});

            #endregion example with Run method


            #region example with use methhod:

            app.Use(async (context, next) => {
                await context.Response.WriteAsync("Response using use-method 1 st middleware\n");

                await context.Response.WriteAsync("End of middleware 1\n");
                await next();
            });

            app.UseWhen(a => a.Request.Query.ContainsKey("role"), a =>
            {
                a.Use(async (branch, next) =>
                {
                    await branch.Response.WriteAsync(" in Branch pipeline with query string condition,:role: the query parameter is ->" + branch.Request.Query["role"] + "\n");

                    await branch.Response.WriteAsync(" End  Branch pipeline with query string condition,:role:\n");
                    await next();
                });

            });

            // Map method 

            app.Map("/map", a =>
            {
                a.Map("/branch", t =>
                {
                    //t.Run(async context =>
                    //{
                    //    await context.Response.WriteAsync("Response of Map/branch middleware\n");

                    //});
                    t.Use(async (context, next) => {
                        await context.Response.WriteAsync("map with use method\n");
                        await next();
                    });

                    t.Run(async context =>
                    {
                        await context.Response.WriteAsync("Response of Map/branch middleware\n");

                    });

                });
                a.Run(async context =>
                {
                    await context.Response.WriteAsync("Response of Map middleware\n");

                });

            });
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Response of Last middleware\n");

            });
            #endregion
        }
    }
}
