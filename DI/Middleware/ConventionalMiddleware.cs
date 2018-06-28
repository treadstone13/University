using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Models;

namespace University.DI.Middleware
{
    public class ConventionalMiddleware
    {
        private readonly RequestDelegate _next;

        public ConventionalMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UniversityContext db)
        {
            var keyValue = context.Request.Query["Key"];

            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                db.Add(new Request()
                {
                    DT = DateTime.Now,
                    MiddlewareActivation = "ConventionalMiddleware",
                    Value = keyValue
                });

                await db.SaveChangesAsync();
            }

            await _next(context);
        }
    }
}
