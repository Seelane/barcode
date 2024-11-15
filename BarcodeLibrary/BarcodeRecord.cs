using System.Text;

namespace BarcodeLibrary
{
    public record BarcodeRecord
    {
        private string _full;
        private string _text;
        private string _barcode;

        public string Full => _full;


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

        private void UpdateBarcode()
        {
            var finalText = new StringBuilder();
            var barcodeHeight = BarcodeUtilities.BarcodeHeight + 2;

            var barcodeLength = BarcodeString.ToString().Length / barcodeHeight;
            var firstLength = barcodeLength / 2;
            var halfInputLength = Text.Length / 2;
            finalText.Append("".PadLeft(firstLength - halfInputLength, ' '));
            finalText.Append(Text);
            finalText.Append("".PadLeft(barcodeLength - firstLength - (Text.Length - halfInputLength), ' '));
            _full = $"{BarcodeString}{finalText}";
        }

        public BarcodeRecord(string text)
        {
            Text = $"* {text} *";
        }

        public override string ToString() {
            return IBarcode.BarcodeType switch
            {
                BarcodeType.Text => Text,
                BarcodeType.Barcode => BarcodeString,
                BarcodeType.Full => Full,
                _ => "Full"
            };
        }
    }
}
