using System;
using System.Threading.Tasks;

using Foundation;
using UIKit;
using Deals3000.Models;
using CoreGraphics;

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
            BackgroundColor = UIColor.Red,
        };
        private UIStackView stackView = new UIStackView()
        {
            TranslatesAutoresizingMaskIntoConstraints = false,
            Distribution = UIStackViewDistribution.FillEqually,
            Alignment = UIStackViewAlignment.Leading,
            Spacing = 5.0f,
            Axis = UILayoutConstraintAxis.Vertical,
            BackgroundColor = UIColor.Purple,
        };
        private UILabel titleLabel = new UILabel()
        {
            Font = UIFont.SystemFontOfSize(14.0f),
            TextColor = UIColor.Black,
            TextAlignment = UITextAlignment.Left,
            LineBreakMode = UILineBreakMode.TailTruncation,
        };
        private UILabel descriptionLabel = new UILabel()
        {
            Font = UIFont.SystemFontOfSize(10.0f),
            TextColor = UIColor.Gray,
            TextAlignment = UITextAlignment.Left,
            LineBreakMode = UILineBreakMode.TailTruncation,
        };

        protected ProductViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            BackgroundColor = UIColor.Green;

            AddSubview(imageView);
            AddSubview(stackView);

            var views = new NSMutableDictionary();
            views.Add(new NSString("i"), imageView);
            views.Add(new NSString("s"), stackView);
            AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|-[i(50)]-5-[s]-|", (NSLayoutFormatOptions)0, null, views));
            AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|-5-[s(50)]-5-|", (NSLayoutFormatOptions)0, null, views));
            AddConstraint(NSLayoutConstraint.Create(imageView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, stackView, NSLayoutAttribute.Top, 1.0f, 0.0f));
            AddConstraint(NSLayoutConstraint.Create(imageView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, stackView, NSLayoutAttribute.Bottom, 1.0f, 0.0f));

            stackView.AddArrangedSubview(titleLabel);
            stackView.AddArrangedSubview(descriptionLabel);
        }

        public override void PrepareForReuse(){
            base.PrepareForReuse();
            Product.ProductImageChanged += null;
            Product = null;
            titleLabel.Text = null;
            descriptionLabel.Text = null;
        }

        public void LoadProduct(Product product){
            this.Product = product;
            if (this.Product.ProductUIImage == null){
                this.Product.ProductImageChanged += (object sender, ProductImageChangedEventArgs args) => {
                    InvokeOnMainThread(() => {
                        imageView.Image = args.NewImage;
                    });
                };
                this.Product.LoadImage();
            }else{
                imageView.Image = this.Product.ProductUIImage;
            }
            titleLabel.Text = this.Product.Name;
            descriptionLabel.Text = this.Product.Description;
        }

    }
}
