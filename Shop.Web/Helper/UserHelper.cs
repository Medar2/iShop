using Microsoft.AspNetCore.Identity;
using Shop.Web.Data.Entities;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Helper
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserHelper( 
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await this.userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await this.userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string OldPassword, string newPassword)
        {
            return await this.userManager.ChangePasswordAsync(user,OldPassword,newPassword);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await this.roleManager.CreateAsync(new IdentityRole
                { 
                    Name = roleName
                });
            }
        }

        public async Task<User> GetUserbyEmailAsync(string email)
        {
            //var user = await userManager.FindByEmailAsync(email);
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await this.userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await this.signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RememberMe,
                false); //investigar el bloqueo y desbloqueo de usuarios
        }


        public async Task LogoutAsync()
        {
            await this.signInManager.SignOutAsync();

        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await this.userManager.UpdateAsync(user);
        }

        public async Task<SignInResult> ValidatePasswordAsync(User user, string password)
        {
            return await this.signInManager.CheckPasswordSignInAsync(
         user,
         password,
         false);
        }

        //Task<IdentityResult> IUserHelper.AddUserAsync(User user, string password)
        //{
        //    throw new NotImplementedException();
        //}

        //Task IUserHelper.AddUserToRoleAsync(User user, string roleName)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IdentityResult> IUserHelper.ChangePasswordAsync(User user, string OldPassword, string newPassword)
        //{
        //    throw new NotImplementedException();
        //}

        //Task IUserHelper.CheckRoleAsync(string roleName)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<User> IUserHelper.GetUserbyEmailAsync(string email)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<bool> IUserHelper.IsUserInRole(User user, string roleName)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<SignInResult> IUserHelper.LoginAsync(LoginViewModel model)
        //{
        //    throw new NotImplementedException();
        //}

        //Task IUserHelper.LogoutAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IdentityResult> IUserHelper.UpdateUserAsync(User user)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<SignInResult> IUserHelper.ValidatePasswordAsync(User user, string password)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
