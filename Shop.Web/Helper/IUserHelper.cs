﻿using Microsoft.AspNetCore.Identity;
using Shop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Helper
{
    public interface IUserHelper
    {
        Task<User> GetUserbyEmailAsync(string email);
        Task<IdentityResult> AddUserAsync(User user, string password);
    }

}
