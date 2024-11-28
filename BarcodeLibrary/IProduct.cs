namespace BarcodeLibrary
{
    /// <summary>
    /// Интерфейс, описывающий товар
    /// </summary>
    public interface IProduct
    {
        int Id { get; set; }
        string Name { get; set; }
        IBarcode Barcode { get; }

    }
}
