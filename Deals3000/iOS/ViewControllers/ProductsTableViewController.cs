using System;
using System.Collections.Specialized;

using Deals3000.ViewModels;
using UIKit;
using Foundation;

namespace Deals3000.iOS.ViewControllers
{
    public partial class ProductsTableViewController : UITableViewController
    {

        public ProductsViewModel ViewModel { get; }

        public ProductsTableViewController( ProductsViewModel baseViewModel ) : base("ProductsTableViewController", null) {
            this.ViewModel = baseViewModel;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            Title = ViewModel.Title;

            ViewModel.PropertyChanged += IsBusyPropertyChanged;
            ViewModel.Products.CollectionChanged += ItemsCollectionChanged;

            TableView.RegisterClassForCellReuse(typeof(ProductViewCell), "ProductViewCell");
        }

        void IsBusyPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e){
            
        }

        void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e){
            InvokeOnMainThread(() => TableView.ReloadData());
        }

    }

    class ProductsDataSource : UITableViewSource
    {

        ProductsViewModel viewModel;

        public ProductsDataSource(ProductsViewModel viewModel){
            this.viewModel = viewModel;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            ProductViewCell cell = (ProductViewCell) tableView.DequeueReusableCell("ProductViewCell");
            var product = viewModel.Products[indexPath.Row];
            cell.LoadProduct(product);
            return cell;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return viewModel.Products.Count;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }

}

