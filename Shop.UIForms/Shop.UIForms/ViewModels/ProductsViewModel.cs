using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Web.Common.Models;
using Web.Common.Services;
using Xamarin.Forms;

namespace Shop.UIForms.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private ApiService apiService;
        private ObservableCollection<Product> products;
        private bool isRefreshing;

        public ObservableCollection<Product> Products
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }


        //Activiti indicator
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref isRefreshing, value); }
        }

        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            IsRefreshing = true;
            //var response = await this.apiService.GetListAsync<Product>(
            //    "https://devshop.azurewebsites.net",
            //    "/api",
            //    "/Products");
            
            //var url = Application.Current.Resources["UrlAPI"].ToString();
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<Product>(
                url,
                "/api",
                "/Products",
                "bearer",
                MainViewModel.GetInstance().Token.Token);


            IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            var products = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(products);
        }
    }

}
