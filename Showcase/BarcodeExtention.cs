using BarcodeLibrary;
using Showcase_L2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showcase
{
    public static class BarcodeExtention
    {
        public static void UpdateBarcode<T>(this T product, IShowcase<T> showcase) where T : class, IProduct
        {
            int index = showcase.SearchPositionById(product.Id);
            string newText = $"{product.Id} {showcase.Id} {index}";
            product.Barcode.Text = newText;
        }
    }
}
