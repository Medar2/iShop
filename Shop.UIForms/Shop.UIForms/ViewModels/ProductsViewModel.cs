using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Shop.Common.Models;
using Shop.Common.Services;
using Xamarin.Forms;

namespace Shop.UIForms.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private ApiService apiService;
        private List<Product> myProducts;

        //private ObservableCollection<Product> products;
        private ObservableCollection<ProductItemViewModel> products;
        private bool isRefreshing;

        //public ObservableCollection<Product> Products
        public ObservableCollection<ProductItemViewModel> Products
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

            this.myProducts = (List<Product>)response.Result;
            //this.Products = new ObservableCollection<ProductItemViewModel>(myProducts.OrderBy(p => p.Name));
            this.RefresProductsList();

        }
        public void AddProductToList(Product product)
        {
            this.myProducts.Add(product);
            this.RefresProductsList();
        }

        public void UpdateProductInList(Product product)
        {
            var previousProduct = this.myProducts.Where(p => p.Id == product.Id).FirstOrDefault();
            if (previousProduct != null)
            {
                this.myProducts.Remove(previousProduct);
            }

            this.myProducts.Add(product);
            this.RefresProductsList();
        }

        public void DeleteProductInList(int productId)
        {
            var previousProduct = this.myProducts.Where(p => p.Id == productId).FirstOrDefault();
            if (previousProduct != null)
            {
                this.myProducts.Remove(previousProduct);
            }

            this.RefresProductsList();
        }

        private void RefresProductsList()
        {
            this.Products = new ObservableCollection<ProductItemViewModel>
                (myProducts.Select(p => new ProductItemViewModel
            {
                Id = p.Id,
                ImagenUrl = p.ImagenUrl,
                ImageFullPath = p.ImageFullPath,
                IsAvailabe = p.IsAvailabe,
                LastPurchase = p.LastPurchase,
                LastSale = p.LastSale,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                User = p.User
            })
            .OrderBy(p => p.Name)
            .ToList());
        }

    }

}
