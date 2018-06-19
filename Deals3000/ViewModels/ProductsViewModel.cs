using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using Deals3000.Models;
using Deals3000.Services;
using Deals3000.Helpers;
using System.Threading.Tasks;

namespace Deals3000.ViewModels
{
    public class ProductsViewModel: BaseViewModel
    {

        public ObservableCollection<Product> Products { get; set; }
        public Command LoadProductsCommand { get; set; }

        private IDataStore<Product> DataStore = ServiceLocator.Instance.Get<IDataStore<Product>>() ??  new MockDataStore();

        public ProductsViewModel()
        {
            Products = new ObservableCollection<Product>();
            Title = "List of products";
            LoadProductsCommand = new Command(async () => await ExecuteLoadProductsCommand());
        }

        async Task ExecuteLoadProductsCommand(){

            if (IsBusy){
                return;
            }

            IsBusy = true;

            try{
                Products.Clear();
                var result = await DataStore.GetItemsAsync(true);
                List<Product> products = (List<Product>) result;
                foreach (var item in products){
                    Products.Add(item);
                }

            }catch(Exception ex){
                Debug.WriteLine(ex);
            }finally{
                IsBusy = false;
            }

        }

    }
}
