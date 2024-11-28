using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeLibrary
{
    public interface IBarcode
    {
        string Text { get; set; }
        string BarcodeString { get; }
        string Full {  get; }
        public static BarcodeType BarcodeType { get; set; } = BarcodeType.Full;
    }
}
