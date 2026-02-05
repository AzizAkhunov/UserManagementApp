using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using UserManagementApp.Data;
using UserManagementApp.Enums;

namespace UserManagementApp.Middleware
{
    public class UserStatusMiddleware
    {
        private readonly RequestDelegate _next;

        public UserStatusMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AppDbContext db)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId != null)
                {
                    var user = await db.Users.FindAsync(int.Parse(userId));

                    if (user == null || user.Status == UserStatus.Blocked)
                    {
                        await context.SignOutAsync();
                        context.Response.Redirect("/Auth/Login");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
