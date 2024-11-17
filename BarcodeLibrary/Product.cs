namespace BarcodeLibrary
{
    public abstract class Product : IProduct
    {
        public virtual int Id
        {
            get => _id;
            set
            {
                _id = value;
                Barcode.Text = _id.ToString();
            }
        }
        private int _id;
        public IBarcode Barcode { get; }
        public string Name { get; set; }

        public Product(int id, string name, IBarcode barcode)
        {
            _id = id;
            Name = name;
            Barcode = barcode;
        }
        public override string ToString() => $"Type: {GetType().Name} \nId: {Id} \nName: {Name} \n";
    }
}
