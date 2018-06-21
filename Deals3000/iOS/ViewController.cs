using System;

using UIKit;
using Deals3000.Models;
using Deals3000.ViewModels;
using Deals3000.iOS.ViewControllers;
using Deals3000.Services;

namespace Deals3000.iOS
{
    public partial class ViewController : UIViewController
    {
        int count = 1;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        ProductsViewModel viewModel;
        Product Product;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            Button.AccessibilityIdentifier = "myButton";
            this.viewModel = new ProductsViewModel();
            this.viewModel.LoadProductsCommand.Execute(null);
            this.viewModel.Products.CollectionChanged += (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => {
                Console.WriteLine("CollectionChanged");
                if (Product == null)
                {
                    didSetProduct((Product)e.NewItems[0]);
                }


            };

            Button.TouchUpInside += delegate
            {
                var title = string.Format("{0} clicks!", count++);
                Button.SetTitle(title, UIControlState.Normal);
                PresentViewController(new GenericTableViewController(), true, null);
            };

        }

        private void didSetProduct(Product product){
            Product = product;
            Product.ProductImageChanged += (object sender, ProductImageChangedEventArgs args) => {
                Console.WriteLine($"##### -> {Product.ProductUIImage == null}");
                ImageView.Image = Product.ProductUIImage;

            };
            Product.LoadImage();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.		
        }
    }
}
