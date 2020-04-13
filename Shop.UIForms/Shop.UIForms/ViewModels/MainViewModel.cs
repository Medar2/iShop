using System;
using System.Collections.Generic;
using System.Text;
using Web.Common.Models;

namespace Shop.UIForms.ViewModels
{
    public class MainViewModel
    {
        //Singuenton??
        private static MainViewModel instance;
        public TokenResponse Token { get; set; }
        //Se registran todas la view models a usuar
        public LoginViewModel Login { get; set; }
        public ProductsViewModel Products { get; set; }


        public MainViewModel()
        {
            //this.Login = new LoginViewModel(); //No Recomendado 

            instance = this;
            this.Login = new LoginViewModel();
        }
        public static MainViewModel GetInstance()
        {
            if(instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
    }
}
