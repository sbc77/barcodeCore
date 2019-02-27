using System;
using System.Collections.Generic;
using BarcodeCore.Barcodes;

namespace BarcodeCore
{
    public static class BarcodeFactory
    {
        private static IDictionary<BarcodeTypes, Type> barcodes = new Dictionary<BarcodeTypes, Type>
        {
            {BarcodeTypes.Ean_128, typeof(Ean128)},
            {BarcodeTypes.Gs1_128, typeof(Gs128)},
        };

        public static IBarcode Create(BarcodeTypes type)
        {
            return Activator.CreateInstance(barcodes[type]) as IBarcode;
        }
    }
}
