using System.Text;

namespace BarcodeLibrary
{
    public class Barcode : IBarcode
    {
        private string _full;
        private string _text;
        private string _barcode;

        public string Text
        {
            get => _text;
            set
            {
                if (_text == value) return;
                _barcode = BarcodeUtilities.GenerateBarcode(value);
                _text = value;
                UpdateBarcode();
            }
        }
        public string BarcodeString => _barcode;

        public string Full => _full;

        private void UpdateBarcode()
        {
            var finalText = new StringBuilder();
            var barcodeHeight = BarcodeUtilities.BarcodeHeight + 2;

            var barcodeLength = BarcodeString.ToString().Length / barcodeHeight;
            var firstLength = barcodeLength / 2;
            var halfInputLength = _text.Length / 2;
            finalText.Append("".PadLeft(firstLength - halfInputLength, ' '));
            finalText.Append(_text);
            finalText.Append("".PadLeft(barcodeLength - firstLength - (_text.Length - halfInputLength), ' '));
            _full = $"{BarcodeString}{finalText}";
        }

        public Barcode(string text)
        {
            Text = text;
        }

        public override string ToString() => _full;
    }
}
