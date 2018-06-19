using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using Newtonsoft.Json;

using Deals3000.Models;

namespace Deals3000.Services
{
    public class MockDataStore: IDataStore<Product>
    {

        List<Product> products = new List<Product>();
        const string fileName = "/Users/hla/Documents/Github/Deals3000/Deals3000/products.json";
        private IDataStoreDelegate dataStoreDelegate;

        public MockDataStore(IDataStoreDelegate dataStoreDelegate = null)
        {
            this.dataStoreDelegate = dataStoreDelegate;
        }

        public async void fetchItems(bool forceRefresh = false)
        {
            
            try{
                using(StreamReader streamReader = new StreamReader(fileName)){
                    string json = await streamReader.ReadToEndAsync();
                    Dictionary<string, object> values = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                    products = JsonConvert.DeserializeObject<List<Product>>(values["data"].ToString());
                }

            } catch (Exception ex){
                if (dataStoreDelegate != null)
                {
                    dataStoreDelegate.didFetchData(false);
                }
                Console.WriteLine($"An error happened : {ex.ToString()}");
                return;
            }
            if (dataStoreDelegate != null)
            {
                dataStoreDelegate.didFetchData(true);
            }

        }

        public async Task<bool> AddItemAsync(Product item)
        {
            products.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Product item)
        {
            var _product = products.Where((Product arg) => arg.Id == item.Id).FirstOrDefault();
            products.Remove(_product);
            products.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var _product = products.Where((Product arg) => arg.Name == id).FirstOrDefault();
            products.Remove(_product);

            return await Task.FromResult(true);
        }

        public async Task<Product> GetItemAsync(string id)
        {
            return await Task.FromResult(products.FirstOrDefault(s => s.Name == id));
        }

        public async Task<IEnumerable<Product>> GetItemsAsync(bool forceRefresh = false)
        {

            if (forceRefresh) {

                try
                {
                    using (StreamReader streamReader = new StreamReader(fileName))
                    {
                        string json = await streamReader.ReadToEndAsync();
                        Dictionary<string, object> values = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                        products = JsonConvert.DeserializeObject<List<Product>>(values["data"].ToString());
                    }

                }
                catch (Exception ex)
                {
                    if (dataStoreDelegate != null)
                    {
                        dataStoreDelegate.didFetchData(false);
                    }
                    Console.WriteLine($"An error happened : {ex.ToString()}");
                    return null;
                }
            }

            return await Task.FromResult(products);
        }
    }
}
