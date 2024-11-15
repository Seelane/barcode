namespace BarcodeLibrary
{
    // Интерфейс, описывающий товар
    public interface IProduct
    {
        int Id { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        IBarcode Barcode { get; }
    }
}
