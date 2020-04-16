
namespace Shop.UIForms.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Views;
    using Xamarin.Forms;

    public class MenuItemViewModel : Shop.Common.Models.Menu
    {
        //public ICommand SelectMenuCommand { get { return new RelayCommand(this.SelectMenu); } }
        public ICommand SelectMenuCommand => new RelayCommand(this.SelectMenu); 
        private async void SelectMenu()
        {
            App.Master.IsPresented = false;
            var mainViewNModel = MainViewModel.GetInstance();
            switch (this.PageName)
            {
                case "AboutPage":
                    await App.Navigator.PushAsync(new AboutPage());
                    break;
                case "SetupPage":
                    await App.Navigator.PushAsync(new SetupPage());
                    break;
                default:
                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
            }
        }
    }
}
