namespace BarcodeLibrary
{
    public class InkjetPrinter
        : Product
    {
        public double Price { get; set; }

        public int Count { get; set; }

        public bool IsColor { get; set; }

        public InkjetPrinter(int id, string name, string type, bool isColor, double price, int count)
            : base(id, name, type)
        {
            Price = price;
            Count = count;
            IsColor = isColor;
        }

        public override string ToString() 
            => $"{base.ToString()}IsColor: {IsColor} \nPrice: {Price} \nCount: {Count} \nBarcode:\n{Barcode_Lab2}";
    }
}
