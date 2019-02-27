using System;
using System.IO;
using Xunit;

namespace BarcodeCore.Test
{
    public class BarcodeTests
    {
        [Theory]
        [InlineData("(02)07611365331178(37)12(400)20216916", new int[] { 105, 102, 2, 7, 61, 13, 65, 33, 11, 78, 37, 12, 40, 2, 2, 16, 91, 100, 22, 85, 106 })]
        [InlineData("(02)07611365331178(37)112(400)20216916", new int[] { 105, 102, 2, 7, 61, 13, 65, 33, 11, 78, 37, 11, 24, 0, 20, 21, 69, 16, 67, 106 })]
        public void GS1_128Test(string barcode, int[] codes)
        {
            var b = BarcodeFactory.Create(BarcodeTypes.Gs1_128);
            var result = b.GetCodes(barcode);

            Assert.Equal(codes, result);
        }

        [Theory]
        [InlineData("123456abcde", new int[] { 104, 17, 18, 19, 20, 21, 22, 65, 66, 67, 68, 69, 54, 106 })]
        [InlineData("12345abcde", new int[] { 104, 17, 18, 19, 20, 21, 65, 66, 67, 68, 69, 102, 106 })]
        public void Ean128(string barcode, int[] codes)
        {
            var b = BarcodeFactory.Create(BarcodeTypes.Ean_128);
            var result = b.GetCodes(barcode);

            Assert.Equal(codes, result);
        }

        [Fact]
        public void CreateSvgBarcodeTest()
        {
            var barcode = BarcodeFactory.Create(BarcodeTypes.Gs1_128);
            var barcodeTxt = "(02)07611365331178(37)112(400)20216916";
            var scale = 1.3;
            var svg = string.Empty;

            barcode.OnRenderBar = (bar) =>
            {
                svg += $"<rect x='{bar.X * scale + 20}' y='20' height='110' width='{bar.Width * scale}' />{Environment.NewLine}";
            };

            barcode.Render(barcodeTxt);

            svg = $@"<?xml version='1.0' ?>
                     <svg xmlns='http://www.w3.org/2000/svg' xmlns:xlink= 'http://www.w3.org/1999/xlink' width='600' height='170'>
                        {svg}
                        <text x='20' y='150' >{barcodeTxt}</text>
                     </svg>";

            File.WriteAllText("barcodeTest.svg", svg);
        }
    }
}
