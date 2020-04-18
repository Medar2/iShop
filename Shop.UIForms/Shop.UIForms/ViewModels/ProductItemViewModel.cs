namespace Shop.UIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Shop.Common.Models;
    using Shop.UIForms.Views;
    using System.Windows.Input;
    public class ProductItemViewModel : Product
    {
        public ICommand SelectProductCommand => new RelayCommand(this.SelectProduct);

        private async void SelectProduct()
        {
            MainViewModel.GetInstance().EditProduct = new EditProductViewModel((Product)this);
            await App.Navigator.PushAsync(new EditProductPage());
        }

    }
}
