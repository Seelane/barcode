namespace BarcodeLibrary
{
    public abstract class Product : IProduct
    {
        public event EventHandler<IdChangedEventArgs>? IdChanged = delegate { };

        public virtual int Id
        {
            get => _id;
            set
            {
                int tmp = _id;
                _id = value;
                Barcode.Text = _id.ToString();
                OnIdChanged(_id, tmp);
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
        protected virtual void OnIdChanged(int oldId, int newId)
        {
            IdChanged?.Invoke(this, new IdChangedEventArgs(oldId, newId));
        }
        public override string ToString() => $"Type: {GetType().Name} \nId: {Id} \nName: {Name} \n";
    }
}
