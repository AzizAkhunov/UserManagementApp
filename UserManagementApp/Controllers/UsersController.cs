using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementApp.Data;
using UserManagementApp.Enums;

namespace UserManagementApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
                .OrderByDescending(u => u.LastLoginTime)
                .ToListAsync();

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Block(int[] ids)
        {
            var users = await _context.Users.Where(u => ids.Contains(u.Id)).ToListAsync();
            users.ForEach(u => u.Status = UserStatus.Blocked);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Unblock(int[] ids)
        {
            var users = await _context.Users.Where(u => ids.Contains(u.Id)).ToListAsync();
            users.ForEach(u => u.Status = UserStatus.Active);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int[] ids)
        {
            var users = await _context.Users.Where(u => ids.Contains(u.Id)).ToListAsync();
            _context.Users.RemoveRange(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUnverified()
        {
            var users = await _context.Users
                .Where(u => u.Status == UserStatus.Unverified)
                .ToListAsync();

            _context.Users.RemoveRange(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
