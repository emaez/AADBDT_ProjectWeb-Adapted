using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NRAKO.IntegrationTest
{
    class FakeUserFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, AdminData.Id),
                new Claim(ClaimTypes.Name, AdminData.Name),
                //new Claim(ClaimTypes.Email, "admin@admin.com"),
                new Claim(ClaimTypes.Role, AdminData.Role)
            }));
            await next();
        }
    }
}
