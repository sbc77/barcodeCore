using System;

namespace BarcodeCore
{
    public interface IBarcode
    {
        BarcodeTypes BarcodeType { get; }

        void Render(string barcode);

        int[] GetCodes(string barcode);

        Action<BarcodeBar> OnRenderBar { set; }
    }
}
