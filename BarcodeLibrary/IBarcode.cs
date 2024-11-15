using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeLibrary
{
    public interface IBarcode
    {
        public string Text { get; set; }
        public string BarcodeString { get; }
        public string Full {  get; }
        public static BarcodeType BarcodeType { get; set; } = BarcodeType.Full;
    }
}
