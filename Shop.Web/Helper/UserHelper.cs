using Microsoft.AspNetCore.Identity;
using Shop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Helper
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> userManager;

        public UserHelper( UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await this.userManager.CreateAsync(user, password);
        }

        public async Task<User> GetUserbyEmailAsync(string email)
        {
            //var user = await userManager.FindByEmailAsync(email);
            return await userManager.FindByEmailAsync(email);
        }
    }
}
