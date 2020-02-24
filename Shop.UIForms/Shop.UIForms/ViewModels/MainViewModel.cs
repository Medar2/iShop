using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.UIForms.ViewModels
{
    public class MainViewModel
    {
        //Singuenton??
        private static MainViewModel instance; 
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
