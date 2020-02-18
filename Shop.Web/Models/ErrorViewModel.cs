using Microsoft.AspNetCore.Http;
using Shop.Web.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public class ProductViewModel : Product
        {
            [Display(Name = "Image")]
            public IFormFile ImageFile { get; set; }
        }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}