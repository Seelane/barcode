using System.Security.Cryptography;

namespace BarcodeLibrary
{
    sealed public class InkjetPrinter : Printer
    {
        public override int Id
        {
            get => _id;
            set
            {
                _id = value;
            }
        }
        private int _id;
        public string InkType { get; set; }
        public int NumberOfColors { get; set; }
        public new BarcodeRecord Barcode { get; }
        public InkjetPrinter(int id, string name, string description, bool isColor, double price, int count, string inkType, int numberOfColors) 
            : base(id, name, new BarcodeRecord(id.ToString()), description, isColor, price, count)
        {
            Id = id;
            InkType = inkType;
            NumberOfColors = numberOfColors;
            Barcode = new BarcodeRecord(id.ToString());
        }
        public override string ToString() => $"{ToStringCh1()}InkType: {InkType} \nNumberOfColors: {NumberOfColors}\n" +
            $"{ToStringCh2()}Barcode: \n{Barcode}";
    }
}
