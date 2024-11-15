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
        public int Count { get; set; }
        public bool IsColor { get; set; }
        public double Price { get; set; }
        private int _id;
        public IBarcode Barcode { get; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Barcode_Lab2 { get => Barcode.ToString(); }

        public Product(int id, string name, string description)
        {
            _id = id;
            Name = name;
            Description = description;
            Barcode = new Barcode(_id.ToString());
        }
        public string Type => "Товар";
        public string ToStringBased()
        {
            return $"Id: {Id} \nName: {Name} \nDescription: {Description}\n";
        }
        public override string ToString()
        {
            return $"Type: {Type}\n{this.ToStringBased()}";
        }
    }
}
