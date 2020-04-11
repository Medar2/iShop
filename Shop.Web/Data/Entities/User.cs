﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data.Entities
{
    public class User:IdentityUser
    {
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} caraters length.")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} caraters length.")]
        public string LastName { get; set; }
        
        [MaxLength(100,ErrorMessage ="The field {0} only can contain {1} caraters length.")]
        public string Address { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }

    }
}
