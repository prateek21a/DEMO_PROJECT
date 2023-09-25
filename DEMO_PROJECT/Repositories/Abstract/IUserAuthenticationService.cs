using DEMO_PROJECT.Models.Domain;
using DEMO_PROJECT.Models.DTO;

namespace DEMO_PROJECT.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {

        Task<Status> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(RegistrationModel model);
        Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username);
        IEnumerable<ApplicationUser> GetAllUsers();
        Task<Status> AssignUserToRoleAsync(string userId, string roleName);



        Task<Status> DeleteUserAsync(string userId);


    }

}
