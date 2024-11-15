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

        public string Type => "Струйный принтер";
        public string ToStringIP() 
            => $"{base.ToStringBased()}IsColor: {IsColor} \nPrice: {Price} \nCount: {Count} \nBarcode:\n{Barcode_Lab2}";
        public override string ToString()
            => $"Type: {Type} \n{this.ToStringIP()}";
    }
}
