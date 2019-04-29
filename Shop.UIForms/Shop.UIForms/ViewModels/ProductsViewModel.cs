namespace Shop.UIForms.ViewModels
{
    using Shop.Common.Models;
    using Shop.Common.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class ProductsViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        private ObservableCollection<Product> products;
        private bool isRefreshing;

        public bool IsRefreshing
        {
            get => this.isRefreshing;
            set => this.SetValue(ref this.isRefreshing, value);
        }
        public ObservableCollection<Product> Products
        {
            get => this.products;
            set => this.SetValue(ref this.products, value);
        }

        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProdcuts();
        }

        private async void LoadProdcuts()
        {
            this.IsRefreshing = true;
            Response response = await this.apiService.GetListAsync<Product>(
                "https://shopjonathan.azurewebsites.net",
                "/api",
                "/Products");

            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            List<Product> myProducts = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(myProducts);

        }
    }
}
