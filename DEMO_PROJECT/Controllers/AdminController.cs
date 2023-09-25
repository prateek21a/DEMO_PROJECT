using DEMO_PROJECT.Models.Domain;
using DEMO_PROJECT.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO_PROJECT.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IUserAuthenticationService _authService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(
            IUserAuthenticationService authService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _authService = authService;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Display()
        {
            // Retrieve a list of all users
            var allUsers = _authService.GetAllUsers();

            // Filter the list to exclude users with the "admin" role
            var regularUsers = allUsers.Where(user => !user.FirstName.Contains("John"));

            // Create a dictionary to store user roles
            var userRoles = new Dictionary<string, IList<string>>();

            foreach (var user in regularUsers)
            {
                var roles = await userManager.GetRolesAsync(user);
                userRoles.Add(user.Id, roles);
            }

            ViewBag.UserRoles = userRoles; // Pass the user roles to the view

            return View(regularUsers);
        }


        [HttpPost]
        public async Task<IActionResult> AssignUserToRole(string userId, string roleName)
        {
            // Check if the current user is an admin
            var currentUser = await userManager.GetUserAsync(User);

            if (currentUser == null || !await userManager.IsInRoleAsync(currentUser, "admin"))
            {
                return Forbid();
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                TempData["error"] = "User not found.";
                return RedirectToAction("Display");
            }

            // Remove the user from all roles
            var currentRoles = await userManager.GetRolesAsync(user);
            var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!removeResult.Succeeded)
            {
                TempData["error"] = "Failed to remove user from current roles.";
                return RedirectToAction("Display");
            }

            // Use UserManager to assign the selected role to the user
            var assignResult = await userManager.AddToRoleAsync(user, roleName);

            if (assignResult.Succeeded)
            {
                TempData["msg"] = "Role assigned successfully.";
            }
            else
            {
                TempData["error"] = "Failed to assign role.";
            }

            return RedirectToAction("Display");
        }


        public async Task<IActionResult> DeleteUser(string userId)
        {
            // Delete the user with the specified userId
            var result = await _authService.DeleteUserAsync(userId);

            TempData["msg"] = result.Message;

            return RedirectToAction("Display");
        }
    }
}