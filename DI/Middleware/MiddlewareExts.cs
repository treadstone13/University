using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.DI.Middleware
{
    public static class MiddlewareExts
    {
        public static IApplicationBuilder UseConventionalMiddleware(
        this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ConventionalMiddleware>();
        }

        public static IApplicationBuilder UseIMiddlewareMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MiddlewareRepository>();
        }
    }
}
