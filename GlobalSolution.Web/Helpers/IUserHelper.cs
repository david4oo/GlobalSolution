using GlobalSolution.Web.Data.Entities;
using GlobalSolution.Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace GlobalSolution.Web.Helpers
{
    public interface IUserHelper
    {

        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();
        Task<IdentityResult> UpdateUserAsync(User user);


        Task<bool> DeleteUserAsync(string email);

        Task<SignInResult> ValidatePasswordAsync(User user, string password);





    }
}
