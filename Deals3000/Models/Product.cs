using System;

#if __IOS__
using UIKit;
#endif

namespace Deals3000.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        //public Array categories { get; set; }

        public byte[] ImageBytes { get; set; }

#if __IOS__

        public event ProductImageChangedEvent ProductImageChanged;
        private UIImage _UIImage;
        public UIImage ProductUIImage { 
            get
            {
                return _UIImage;
            }
            set{
                
                if (_UIImage == value){
                    return;
                }
                var oldImage = _UIImage;
                _UIImage = value;
                OnProductUIImageChanged(new ProductImageChangedEventArgs(oldImage, value));

            } 
        }

        protected virtual void OnProductUIImageChanged(ProductImageChangedEventArgs args)
        {
            ProductImageChanged?.Invoke(this, args);
        }
#endif

        public override string ToString()
        {
            return $"Product {Name}, {Description}";
        }
    }

#if __IOS__

    public delegate void ProductImageChangedEvent(object sender, ProductImageChangedEventArgs args);
    public class ProductImageChangedEventArgs: EventArgs
    {
        
        public UIImage OldImage;
        public UIImage NewImage;

        public ProductImageChangedEventArgs(UIImage OldImage, UIImage NewImage)
        {
            this.OldImage = OldImage;
            this.NewImage = NewImage;
        }

    }

#endif

}
