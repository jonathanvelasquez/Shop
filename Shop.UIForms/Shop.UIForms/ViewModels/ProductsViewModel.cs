﻿namespace Shop.UIForms.ViewModels
{
    using Shop.Common.Models;
    using Shop.Common.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;

    public class ProductsViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        private List<Product> myProducts;
        private ObservableCollection<ProductItemViewModel> products;
        private bool isRefreshing;

        public bool IsRefreshing
        {
            get => this.isRefreshing;
            set => this.SetValue(ref this.isRefreshing, value);
        }
        public ObservableCollection<ProductItemViewModel> Products
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

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<Product>(
                url,
                "/api",
                "/Products",
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            this.myProducts = (List<Product>)response.Result;
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
            this.Products = new ObservableCollection<ProductItemViewModel>(myProducts.Select(p => new ProductItemViewModel
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                ImageFullPath = p.ImageFullPath,
                Isavailabe = p.Isavailabe,
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
