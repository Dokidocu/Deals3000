using NUnit.Framework;
using System;
using System.Collections.Generic;
using Deals3000.Models;
using Deals3000.Services;

namespace TestShared
{
    [TestFixture()]
    public class NUnitTestClass
    {
        [Test()]
        public void TestCase()
        {
        }

        [Test]
        public void Fail(){
            MockDataStore mockDataStore = new MockDataStore();
            var products = mockDataStore.GetItemsAsync(true).Result;
            List<Product> _products = (List<Product>) products;
            Console.WriteLine($"##### -> {_products[0].Name}");
        }
    }
}
