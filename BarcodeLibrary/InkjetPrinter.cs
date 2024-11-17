﻿namespace BarcodeLibrary
{
    public class InkjetPrinter
        : Product, IProduct
    {
        public double Price { get; set; }

        public int Count { get; set; }

        public bool IsColor { get; set; }

        public InkjetPrinter(int id, string name, string description, bool isColor, double price, int count)
            : base(id, name, description)
        {
            Price = price;
            Count = count;
            IsColor = isColor;
        }
        public override string ToString()
            => $"{base.ToString()}IsColor: {IsColor} \nPrice: {Price} \nCount: {Count} \nBarcode:\n{Barcode_Lab2}";
    }
}
