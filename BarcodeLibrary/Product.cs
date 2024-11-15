namespace BarcodeLibrary
{
    public abstract class Product : IProduct
    {
        public int Id
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
        public string Type { get; set; }

        public string Barcode_Lab2 { get => Barcode.ToString(); }

        public Product(int id, string name, string type)
        {
            _id = id;
            Name = name;
            Type = type;
            Barcode = new Barcode(_id.ToString());
        }

        public override string ToString()
        {
            return $"Id: {Id} \nName: {Name} \nType: {Type}\n";
        }
    }
}
