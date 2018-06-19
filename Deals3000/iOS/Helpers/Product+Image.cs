using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

using CoreImage;
using Deals3000.Models;
using UIKit;
using Foundation;

namespace Deals3000
{

    public static class MyExtension 
    {
        public static int Test(this string str){
            return 0;
        }

        public static async void LoadImage(this Product product){
            if (product.ImageBytes != null)
            {
                var data = NSData.FromArray(product.ImageBytes);
                product.ProductUIImage = await Task.FromResult(new UIImage(data));
            }
            else
            {
                WebClient webClient = new WebClient();
                Uri url = new Uri(product.Image);
                Console.WriteLine($"URL : {product.Image}");
                webClient.DownloadDataCompleted += (s, e) =>
                {
                    byte[] bytes = e.Result;
                    product.ImageBytes = bytes;
                    NSData data = NSData.FromArray(bytes);
                    Console.WriteLine($"---- > {data == null}");
                    UIImage image = UIImage.LoadFromData(data);
                    Console.WriteLine($"IMAGE ---- > {image == null}");
                    product.ProductUIImage = UIImage.LoadFromData(data);
                };
                webClient.DownloadDataAsync(url);
            }
        }

    }

}