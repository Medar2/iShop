using GalaSoft.MvvmLight.Command;
using Shop.UIForms.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Shop.UIForms.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            this.Email = "jcaraballo74@hotmail.com";
                this.Password = "123456";
        }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(Login);

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Arror",
                    "You must enter email",
                    "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Arror",
                    "You must enter password",
                    "Accept");
                return;
            }
            if (!this.Email.Equals("jcaraballo74@hotmail.com") || !this.Password.Equals("123456"))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Email or Password icorrect!!!",
                    "Accept");
                return;
            }
            //await Application.Current.MainPage.DisplayAlert(
            //        "Ok",
            //        "Fuck yeah!!!",
            //        "Accept");

            MainViewModel.GetInstance().Products = new ProductsViewModel(); 
            await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());
        }
        //public ICommand LoginComand
        //{
        //    get
        //    {
        //        return new RelayCommand(Login);
        //    }
        //}


    }
}
