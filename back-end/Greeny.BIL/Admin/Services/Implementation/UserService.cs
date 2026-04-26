using Greeny.BLL.Admin.ModelVM.User;
using Microsoft.AspNetCore.Identity;

namespace Greeny.BLL.Admin.Services.Implementation
{
    //public class UserService : IUserService
    //{

    //        private readonly UserManager<User> _userManager;
    //        private readonly RoleManager<IdentityRole> _roleManager;

    //        public UserService( UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    //        {
    //            _userManager = userManager;
    //            _roleManager = roleManager;
    //        }

    //    public async Task<Response<bool>> DeleteAsync(string id)
    //    {

    //        var user = await _userManager.FindByIdAsync(id);

    //        if (user == null)
    //        {
    //            return new Response<bool>
    //            {
    //                IsSuccess = false,
    //                Message = "User not found"
    //            };
    //        }

    //        user.IsDeleted = true;

    //        await _userManager.UpdateAsync(user);

    //        return new Response<bool>
    //        {
    //            IsSuccess = true,
    //            Data = true,
    //            Message = "User deleted"
    //        };

    //    }

    //    public async Task<Response<IEnumerable<DetailsUserVM>>> GetAllActiveAsync()
    //    {
    //        var users = _userManager.Users
    //            .Where(u => !u.IsDeleted)
    //            .ToList();

    //        var data = new List<DetailsUserVM>();

    //        foreach (var user in users)
    //        {
    //            data.Add(new DetailsUserVM
    //            {
    //                Id = user.Id,
    //                Email = user.Email,
    //                FullName = user.FirstName +" "+ user.LastName,
    //                Address = user.Address,
    //                PhoneNumber = user.PhoneNumber,
    //                ProfilePicture = user.ProfilePicture,
    //            });
    //        }

    //        return new Response<IEnumerable<DetailsUserVM>>
    //        {
    //            IsSuccess = true,
    //            Data = data,
    //            Message = "Users retrieved"
    //        };
    //    }



    //    Task<Response<IEnumerable<DetailsUserVM>>> GetAllDeletedAsync()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    Task<Response<DetailsUserVM>> GetByIdAsync(string id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    Task<Response<bool>> UpdateAsync(UpdateUserVM vm)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
