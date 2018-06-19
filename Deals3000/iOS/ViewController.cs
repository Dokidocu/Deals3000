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

            Button.TouchUpInside += delegate
            {
                var title = string.Format("{0} clicks!", count++);
                Button.SetTitle(title, UIControlState.Normal);
                if (Product == null){
                    Product = viewModel.Products[0];
                    Product.LoadImage();
                }
                Console.WriteLine($"##### -> {Product.ProductUIImage == null}");
            };

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.		
        }
    }
}
