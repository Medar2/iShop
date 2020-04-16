using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Shop.Common.Models;

namespace Shop.UIForms.ViewModels
{
    public class MainViewModel
    {
        //Singuenton??
        private static MainViewModel instance;

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }
        public TokenResponse Token { get; set; }
        //Se registran todas la view models a usuar
        public LoginViewModel Login { get; set; }
        public ProductsViewModel Products { get; set; }


        public MainViewModel()
        {
            //this.Login = new LoginViewModel(); //No Recomendado 

            instance = this;
            //this.Login = new LoginViewModel();
            this.LoadMenus();
        }

       private void LoadMenus()
        {
	        var menus = new List<Menu>
	        {
    	        new Menu
    	        {
        	        Icon = "ic_info",
        	        PageName = "AboutPage",
        	        Title = "About"
    	        },

    	        new Menu
    	        {
        	        Icon = "ic_phonelink_setup",
        	        PageName = "SetupPage",
        	        Title = "Setup"
    	        },

    	        new Menu
    	        {
        	        Icon = "ic_exit_to_app",
        	        PageName = "LoginPage",
        	        Title = "Close session"
    	        }
	        };

            this.Menus = new ObservableCollection<MenuItemViewModel>(menus.Select(m => new MenuItemViewModel
            {
                Icon = m.Icon,
                PageName = m.PageName,
                Title = m.Title
            }).ToList());

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
