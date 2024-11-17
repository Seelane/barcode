namespace BarcodeLibrary
{
    public class Printer
        : Product
    {
        public string Description { get; set; }
        public int Count { get; set; }
        public bool IsColor { get; set; }
        public double Price { get; set; }
        public Printer(int id, string name, string description, bool isColor, double price, int count)
            : this(id, name, new Barcode(id.ToString()), description, isColor, price, count)
        {          
        }


        public Printer(int id, string name, IBarcode barcode, string description, bool isColor, double price, int count)
            : base(id, name, barcode)
        {
            Description = description;
            Price = price;
            Count = count;
            IsColor = isColor;
        }


        protected string ToStringCh1()
            => $"{base.ToString()}Description: {Description} \nIsColor: {IsColor} \n";
        protected string ToStringCh2() => $"Price: {Price}\nCount: {Count}\n";
        public override string ToString() => $"{ToStringCh1()}{ToStringCh2()}Barcode:\n{Barcode}";
    }
}
