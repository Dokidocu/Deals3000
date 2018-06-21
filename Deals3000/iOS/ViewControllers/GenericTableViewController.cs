using System;

using UIKit;
using Foundation;

using Deals3000.ViewModels;
using Deals3000.Models;

namespace Deals3000.iOS.ViewControllers
{
    public partial class GenericTableViewController : UIViewController
    {

        private ProductsViewModel ProductsViewModel = new ProductsViewModel();

        private UITableView tableView = new UITableView
        {
            TranslatesAutoresizingMaskIntoConstraints = false,
            BackgroundColor = UIColor.Blue,
            RowHeight = UITableView.AutomaticDimension,
        };

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.Red;

            tableView.RegisterClassForCellReuse(typeof(ProductViewCell), "ProductViewCell");

            View.AddSubview(tableView);

            var views = new NSMutableDictionary();
            views.Add(new NSString("t"), tableView);
            View.AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|-[t]-|", (NSLayoutFormatOptions)0, null, views));
            View.AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|-[t]-|", (NSLayoutFormatOptions)0, null, views));

            tableView.Source = new ProductsTableSource(ProductsViewModel);

            ProductsViewModel.LoadProductsCommand.Execute(null);
            ProductsViewModel.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>  {
                tableView.ReloadData();
            };

            // Perform any additional setup after loading the view, typically from a nib.
        }

    }

    class ProductsTableSource : UITableViewSource
    {

        private ProductsViewModel ProductsViewModel { get; set; }

        public ProductsTableSource(ProductsViewModel productsViewModel){
            this.ProductsViewModel = productsViewModel;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            ProductViewCell cell = (ProductViewCell) tableView.DequeueReusableCell("ProductViewCell");
            Product product = ProductsViewModel.Products[indexPath.Row];
            cell.LoadProduct(product);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return ProductsViewModel.Products.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            Console.WriteLine($"{indexPath}");
        }



    }
}

