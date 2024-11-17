namespace BarcodeLibrary
{
    // Интерфейс, описывающий товар
    public interface IProduct
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        IBarcode Barcode { get; }
        double Price { get; set; }
        public int Count { get; set; }
        public bool IsColor { get; set; }
    }
}
