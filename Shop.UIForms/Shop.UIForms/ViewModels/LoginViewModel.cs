using GalaSoft.MvvmLight.Command;
using Shop.UIForms.Views;
using System;
using System.Windows.Input;
using Shop.Common.Models;
using Shop.Common.Services;
using Xamarin.Forms;
using Shop.Common.Helpers;
using Newtonsoft.Json;

namespace Shop.UIForms.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
        public bool IsRemember { get; set; }

        //private readonly ApiService apiService;

        public bool IsRunning
        {
            get => this.isRunning;
            set => this.SetValue(ref this.isRunning, value);
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetValue(ref this.isEnabled, value);
        }

        public LoginViewModel()
        {
            this.apiService = new ApiService();
            //this.Email = "jcaraballo74@hotmail.com";
            //this.Password = "123456";
            IsEnabled = true;
            this.IsRemember = true;
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
            //if (!this.Email.Equals("jcaraballo74@hotmail.com") || !this.Password.Equals("123456"))
            //{
            //    await Application.Current.MainPage.DisplayAlert(
            //        "Error",
            //        "Email or Password icorrect!!!",
            //        "Accept");
            //    return;
            //}
            //await Application.Current.MainPage.DisplayAlert(
            //        "Ok",
            //        "Fuck yeah!!!",
            //        "Accept");
            this.IsRunning = true;
            this.IsEnabled = false;

            var request = new TokenRequest
            {
                Password = this.Password,
                Username = this.Email
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetTokenAsync(
                url,
                "/Account",
                "/CreateToken",
                request);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email or password incorrect.", "Accept");
                return;
            }

            var token = (TokenResponse)response.Result;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.Products = new ProductsViewModel();
            //await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());
            Application.Current.MainPage = new MasterPage();
            mainViewModel.UserEmail = this.Email;
            mainViewModel.UserPassword = this.Password;

            Settings.IsRemember = this.IsRemember;
            Settings.UserEmail = this.Email;
            Settings.UserPassword = this.Password;
            Settings.Token = JsonConvert.SerializeObject(token);


        }


    }
    //public ICommand LoginComand
    //{
    //    get
    //    {
    //        return new RelayCommand(Login);
    //    }
    //}
}
