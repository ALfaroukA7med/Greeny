

using Greeny.BLL.Abstraction;
using Greeny.BLL.Helper;
using Greeny.BLL.ModelVM.User;
using Microsoft.AspNetCore.Identity;

namespace Greeny.BLL.Services.Implementation
{
    public class UserService : IUserService
    {

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Response<bool>> DeleteAsync(string id)
        {

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return Response<bool>.Fail(UserError.NotFound);
            }

            user.IsDeleted = true;

            await _userManager.UpdateAsync(user);

            return Response<bool>.Success(true);
        }

        public async Task<Response<IEnumerable<DetailsUserVM>>> GetAllActiveAsync()
        {
            var users = _userManager.Users
                .Where(u => !u.IsDeleted)
                .ToList();

            var data = new List<DetailsUserVM>();

            foreach (var user in users)
            {
                data.Add(new DetailsUserVM
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber,
                    ProfilePicture = user.ProfilePicture,
                });
            }

            return Response<IEnumerable<DetailsUserVM>>.Success(data);
        }


        public async Task<Response<IEnumerable<DetailsUserVM>>> GetAllDeletedAsync()
        {
            var users = _userManager.Users
                .Where(u => u.IsDeleted)
                .ToList();

            var data = new List<DetailsUserVM>();

            foreach (var user in users)
            {
                data.Add(new DetailsUserVM
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber,
                    ProfilePicture = user.ProfilePicture,
                });
            }

            return Response<IEnumerable<DetailsUserVM>>.Success(data);
        }

        public async Task<Response<DetailsUserVM>> GetByIdAsync(string id)
        {
            var user = _userManager.Users
                .FirstOrDefault(u =>u.Id==id && !u.IsDeleted);
            if (user == null)
            {
                return Response<DetailsUserVM>.Fail(UserError.NotFound);
            }

            var data = new DetailsUserVM{ 
                    Id = user.Id,
                    Email = user.Email,
                     FirstName = user.FirstName,
                     LastName = user.LastName,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber,
                    ProfilePicture = user.ProfilePicture,
                };


            return Response<DetailsUserVM>.Success(data);
        }

        public async Task<Response<bool>> UpdateAsync(UpdateUserVM vm)
        {
            var user = await _userManager.FindByIdAsync(vm.Id);

            if (user == null || user.IsDeleted)
            {
                return Response<bool>.Fail(UserError.NotFound);
            }

            user.FirstName = vm.FirstName;
            user.LastName = vm.LastName;
            user.PhoneNumber = vm.PhoneNumber;
            user.Address = vm.Address;
            // Upload Image
            if (vm.Picture != null)
            {
                user.ProfilePicture = Upload.UploadFile("Files", vm.Picture);
            }

            var result = await _userManager.UpdateAsync(user);

            return Response<bool>.Success(true);
        }
    }
}
