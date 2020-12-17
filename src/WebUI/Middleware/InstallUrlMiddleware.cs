using Microsoft.AspNetCore.Http;
using PoshtaBy.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoshtaBy.WebUI.Middleware
{
    public class InstallUrlMiddleware
    {
        private readonly RequestDelegate _next;
        
        public InstallUrlMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IWebHelper webHelper)
        {
            if (!DataSettingsHelper.DatabaseIsInstalled())
            {
                var installUrl = string.Format("{0}install", webHelper.GetAgencyLocation());
                if (!webHelper.GetThisPageUrl(false).StartsWith(installUrl, StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.Redirect(installUrl);
                    return;
                }
            }

            await _next(context);
        }
    }
}
