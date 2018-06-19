using System;
using System.Threading.Tasks;

using Foundation;
using UIKit;
using Deals3000.Models;

namespace Deals3000.iOS.ViewControllers
{
    public partial class ProductViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("ProductViewCell");
        public static readonly UINib Nib;

        private Product Product { get; set; }
        private UIImageView imageView = new UIImageView()
        {
            TranslatesAutoresizingMaskIntoConstraints = false,
            ContentMode = UIViewContentMode.ScaleAspectFit,
        };

        static ProductViewCell()
        {
            Nib = UINib.FromName("ProductViewCell", NSBundle.MainBundle);
        }

        protected ProductViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        [Export("awakeFromNib")]
        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            imageView.TranslatesAutoresizingMaskIntoConstraints = false;
            AddSubview(imageView);
            var views = new NSMutableDictionary();
            views.Add(new NSString("i"), imageView);
            AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|-[i]|", (NSLayoutFormatOptions)0, null, views));
            AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|-[i]-|", (NSLayoutFormatOptions)0, null, views));
            AddConstraint(NSLayoutConstraint.Create(imageView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, this, NSLayoutAttribute.Width, 1.0f, 50.0f));

        }

        public override void PrepareForReuse(){
            base.PrepareForReuse();
            Product.ProductImageChanged += null;
            Product = null;
        }

        public void LoadProduct(Product product){
            this.Product = product;
            this.Product.ProductImageChanged += (object sender, ProductImageChangedEventArgs args) => {
                InvokeOnMainThread(() => {
                    imageView.Image = args.NewImage;
                });
            };
            this.Product.LoadImage();
        }

    }
}
