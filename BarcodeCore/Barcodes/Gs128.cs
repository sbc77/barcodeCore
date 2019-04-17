using System;
using System.Collections.Generic;
using System.Linq;
using BarcodeCore.Barcodes.GS1;

namespace BarcodeCore.Barcodes
{
    public class Gs128 : Ean128
    {
        #region data
        private readonly static IEnumerable<Gs1AppCode> codes = new List<Gs1AppCode>
        {
            new Gs1AppCode( "00", 0, "Serial Shipping Container Code (SSCC)", "N2+N18" ),
            new Gs1AppCode( "01", 0, "Global Trade Item Number (GTIN)", "N2+N14" ),
            new Gs1AppCode( "02", 0, "GTIN of contained trade items", "N2+N14" ),
            new Gs1AppCode( "10", 0, "Batch or lot number", "N2+X..20" ),
            new Gs1AppCode( "11", 0, "Production date (YYMMDD)", "N2+N6" ),
            new Gs1AppCode( "12", 0, "Due date (YYMMDD)", "N2+N6" ),
            new Gs1AppCode( "13", 0, "Packaging date (YYMMDD)", "N2+N6" ),
            new Gs1AppCode( "15", 0, "Best before date (YYMMDD)", "N2+N6" ),
            new Gs1AppCode( "16", 0, "Sell by date (YYMMDD)", "N2+N6" ),
            new Gs1AppCode( "17", 0, "Expiration date (YYMMDD)", "N2+N6" ),
            new Gs1AppCode( "20", 0, "Internal product variant", "N2+N2" ),
            new Gs1AppCode( "21", 0, "Serial number", "N2+X..20" ),
            new Gs1AppCode( "22", 0, "Consumer product variant", "N2+X..20" ),
            new Gs1AppCode( "30", 0, "Variable count of items (variable measure trade item)", "N2+N..8" ),
            new Gs1AppCode( "37", 0, "Count of trade items or trade item pieces contained in a logistic unit", "N2+N..8" ),
            new Gs1AppCode( "90", 0, "Information mutually agreed between trading partners", "N2+X..30" ),
            new Gs1AppCode( "91", 8, "Company internal information", "N2+X..90" ),
            new Gs1AppCode( "240", 0, "Additional product identification assigned by the manufacturer", "N3+X..30" ),
            new Gs1AppCode( "241", 0, "Customer part number", "N3+X..30" ),
            new Gs1AppCode( "242", 0, "Made-to-Order variation number", "N3+N..6" ),
            new Gs1AppCode( "243", 0, "Packaging component number", "N3+X..20" ),
            new Gs1AppCode( "250", 0, "Secondary serial number", "N3+X..30" ),
            new Gs1AppCode( "251", 0, "Reference to source entity", "N3+X..30" ),
            // new Gs1AppCode( 253, 0, "Global Document Type Identifier (GDTI)", "N3+N13+X..17" ),
            new Gs1AppCode( "254", 0, "GLN extension component", "N3+X..20" ),
            // new Gs1AppCode( 255, 0, "Global Coupon Number (GCN)", "N3+N13+N..12" ),
            new Gs1AppCode( "400", 0, "Customers purchase order number", "N3+X..30" ),
            new Gs1AppCode( "401", 0, "Global Identification Number for Consignment (GINC)", "N3+X..30" ),
            new Gs1AppCode( "402", 0, "Global Shipment Identification Number (GSIN)", "N3+N17" ),
            new Gs1AppCode( "403", 0, "Routing code", "N3+X..30" ),
            new Gs1AppCode( "410", 0, "Ship to - Deliver to Global Location Number", "N3+N13" ),
            new Gs1AppCode( "411", 0, "Bill to - Invoice to Global Location Number", "N3+N13" ),
            new Gs1AppCode( "412", 0, "Purchased from Global Location Number", "N3+N13" ),
            new Gs1AppCode( "413", 0, "Ship for - Deliver for - Forward to Global Location Number", "N3+N13" ),
            new Gs1AppCode( "414", 0, "Identification of a physical location - Global Location Number", "N3+N13" ),
            new Gs1AppCode( "415", 0, "Global Location Number of the invoicing party", "N3+N13" ),
            new Gs1AppCode( "416", 0, "GLN of the production or service location", "N3+N13" ),
            new Gs1AppCode( "420", 0, "Ship to - Deliver to postal code within a single postal authority", "N3+X..20" ),
            // new Gs1AppCode( 421, 0, "Ship to - Deliver to postal code with ISO country code", "N3+N3+X..9" ),
            new Gs1AppCode( "422", 0, "Country of origin of a trade item", "N3+N3" ),
            // new Gs1AppCode( 423, 0, "Country of initial processing", "N3+N3+N..12" ),
            new Gs1AppCode( "424", 0, "Country of processing", "N3+N3" ),
            // new Gs1AppCode( 425, 0, "Country of disassembly", "N3+N3+N..12" ),
            new Gs1AppCode( "426", 0, "Country covering full process chain", "N3+N3" ),
            new Gs1AppCode( "427", 0, "Country subdivision Of origin", "N3+X..3" ),
            new Gs1AppCode( "710", 0, "National Healthcare Reimbursement Number (NHRN) - Germany PZN", "N3+X..20" ),
            new Gs1AppCode( "711", 0, "National Healthcare Reimbursement Number (NHRN) - France CIP", "N3+X..20" ),
            new Gs1AppCode( "712", 0, "National Healthcare Reimbursement Number (NHRN) - Spain CN", "N3+X..20" ),
            new Gs1AppCode( "713", 0, "National Healthcare Reimbursement Number (NHRN) - Brasil DRN", "N3+X..20" ),
            new Gs1AppCode( "714", 0, "National Healthcare Reimbursement Number (NHRN) - Portugal AIM", "N3+X..20" ),
            new Gs1AppCode( "3100", 5, "Net weight, kilograms (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3110", 5, "Length or first dimension, metres (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3120", 5, "Width, diameter, or second dimension, metres (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3130", 5, "Depth, thickness, height, or third dimension, metres (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3140", 5, "Area, square metres (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3150", 5, "Net volume, litres (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3160", 5, "Net volume, cubic metres (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3200", 5, "Net weight, pounds (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3210", 5, "Length or first dimension, inches (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3220", 5, "Length or first dimension, feet (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3230", 5, "Length or first dimension, yards (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3240", 5, "Width, diameter, or second dimension, inches (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3250", 5, "Width, diameter, or second dimension, feet (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3260", 5, "Width, diameter, or second dimension, yards (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3270", 5, "Depth, thickness, height, or third dimension, inches (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3280", 5, "Depth, thickness, height, or third dimension, feet (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3290", 5, "Depth, thickness, height, or third dimension, yards (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3300", 5, "Logistic weight, kilograms", "N4+N6" ),
            new Gs1AppCode( "3310", 5, "Length or first dimension, metres", "N4+N6" ),
            new Gs1AppCode( "3320", 5, "Width, diameter, or second dimension, metres", "N4+N6" ),
            new Gs1AppCode( "3330", 5, "Depth, thickness, height, or third dimension, metres", "N4+N6" ),
            new Gs1AppCode( "3340", 5, "Area, square metres", "N4+N6" ),
            new Gs1AppCode( "3350", 5, "Logistic volume, litres", "N4+N6" ),
            new Gs1AppCode( "3360", 5, "Logistic volume, cubic metres", "N4+N6" ),
            new Gs1AppCode( "3370", 5, "Kilograms per square metre", "N4+N6" ),
            new Gs1AppCode( "3400", 5, "Logistic weight, pounds", "N4+N6" ),
            new Gs1AppCode( "3410", 5, "Length or first dimension, inches", "N4+N6" ),
            new Gs1AppCode( "3420", 5, "Length or first dimension, feet", "N4+N6" ),
            new Gs1AppCode( "3430", 5, "Length or first dimension, yards", "N4+N6" ),
            new Gs1AppCode( "3440", 5, "Width, diameter, or second dimension, inches", "N4+N6" ),
            new Gs1AppCode( "3450", 5, "Width, diameter, or second dimension, feet", "N4+N6" ),
            new Gs1AppCode( "3460", 5, "Width, diameter, or second dimension, yard", "N4+N6" ),
            new Gs1AppCode( "3470", 5, "Depth, thickness, height, or third dimension, inches", "N4+N6" ),
            new Gs1AppCode( "3480", 5, "Depth, thickness, height, or third dimension, feet", "N4+N6" ),
            new Gs1AppCode( "3490", 5, "Depth, thickness, height, or third dimension, yards", "N4+N6" ),
            new Gs1AppCode( "3500", 5, "Area, square inches (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3510", 5, "Area, square feet (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3520", 5, "Area, square yards (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3530", 5, "Area, square inches", "N4+N6" ),
            new Gs1AppCode( "3540", 5, "Area, square feet", "N4+N6" ),
            new Gs1AppCode( "3550", 5, "Area, square yards", "N4+N6" ),
            new Gs1AppCode( "3560", 5, "Net weight, troy ounces (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3570", 5, "Net weight (or volume), ounces (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3600", 5, "Net volume, quarts (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3610", 5, "Net volume, gallons U.S. (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3620", 5, "Logistic volume, quarts", "N4+N6" ),
            new Gs1AppCode( "3630", 5, "Logistic volume, gallons U.S.", "N4+N6" ),
            new Gs1AppCode( "3640", 5, "Net volume, cubic inches (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3650", 5, "Net volume, cubic feet (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3660", 5, "Net volume, cubic yards (variable measure trade item)", "N4+N6" ),
            new Gs1AppCode( "3670", 5, "Logistic volume, cubic inches", "N4+N6" ),
            new Gs1AppCode( "3680", 5, "Logistic volume, cubic feet", "N4+N6" ),
            new Gs1AppCode( "3690", 5, "Logistic volume, cubic yards", "N4+N6" ),
            new Gs1AppCode( "3900", 9, "Applicable amount payable or Coupon value, local currency", "N4+N..15" ),
            // new Gs1AppCode( 3910, 9, "Applicable amount payable with ISO currency code", "N4+N3+N..15" ),
            new Gs1AppCode( "3920", 9, "Applicable amount payable, single monetary area (variable measure trade item)", "N4+N..15" ),
            // new Gs1AppCode( 3930, 9, "Applicable amount payable with ISO currency code (variable measure trade item)", "N4+N3+N..15" ),
            new Gs1AppCode( "3940", 3, "Percentage discount of a coupon", "N4+N4" ),
            new Gs1AppCode( "7001", 0, "NATO Stock Number (NSN)", "N4+N13" ),
            new Gs1AppCode( "7002", 0, "UN/ECE meat carcasses and cuts classification", "N4+X..30" ),
            new Gs1AppCode( "7003", 0, "Expiration date and time", "N4+N10" ),
            new Gs1AppCode( "7004", 0, "Active potency", "N4+N..4" ),
            new Gs1AppCode( "7005", 0, "Catch area", "N4+X..12" ),
            new Gs1AppCode( "7006", 0, "First freeze date", "N4+N6" ),
            new Gs1AppCode( "7007", 0, "Harvest date", "N4+N6..12" ),
            new Gs1AppCode( "7008", 0, "Species for fishery purposes", "N4+X..3" ),
            new Gs1AppCode( "7009", 0, "Fishing gear type", "N4+X..10" ),
            new Gs1AppCode( "7010", 0, "Production method", "N4+X..2" ),
            new Gs1AppCode( "7020", 0, "Refurbishment lot ID", "N4+X..20" ),
            new Gs1AppCode( "7021", 0, "Functional status", "N4+X..20" ),
            new Gs1AppCode( "7022", 0, "Revision status", "N4+X..20" ),
            new Gs1AppCode( "7023", 0, "Global Individual Asset Identifier (GIAI) of an assembly", "N4+X..30" ),
            // new Gs1AppCode( 7030, 9, "Number of processor with ISO Country Code", "N4+N3+X..27" ),
            // new Gs1AppCode( 7230, 9, "Certification reference", "N4+X2+X..28" ),
            new Gs1AppCode( "8001", 0, "Roll products (width, length, core diameter, direction, splices)", "N4+N14" ),
            new Gs1AppCode( "8002", 0, "Cellular mobile telephone identifier", "N4+X..20" ),
            // new Gs1AppCode( 8003, 0, "Global Returnable Asset Identifier (GRAI)", "N4+N14+X..16" ),
            new Gs1AppCode( "8004", 0, "Global Individual Asset Identifier (GIAI)", "N4+X..30" ),
            new Gs1AppCode( "8005", 0, "Price per unit of measure", "N4+N6" ),
            // new Gs1AppCode( 8006, 0, "Identification of an individual trade item piece", "N4+N14+N2+N2" ),
            new Gs1AppCode( "8007", 0, "International Bank Account Number (IBAN)", "N4+X..34" ),
            // new Gs1AppCode( 8008, 0, "Date and time of production", "N4+N8+N..4" ),
            new Gs1AppCode( "8009", 0, "Optically Readable Sensor Indicator", "N4+X..50" ),
            new Gs1AppCode( "8010", 0, "Component/Part Identifier (CPID)", "N4+Y..30" ),
            new Gs1AppCode( "8011", 0, "Component/Part Identifier serial number (CPID SERIAL)", "N4+N..12" ),
            new Gs1AppCode( "8012", 0, "Software version", "N4+X..20" ),
            new Gs1AppCode( "8013", 0, "Global Model Number (GMN)", "N4+X..30" ),
            new Gs1AppCode( "8017", 0, "Global Service Relation Number to identify the relationship between an organisation offering services and the provider of services", "N4+N18" ),
            new Gs1AppCode( "8018", 0, "Global Service Relation Number to identify the relationship between an organisation offering services and the recipient of services", "N4+N18" ),
            new Gs1AppCode( "8019", 0, "Service Relation Instance Number (SRIN)", "N4+N..10" ),
            new Gs1AppCode( "8020", 0, "Payment slip reference number", "N4+X..25" ),
            // new Gs1AppCode( 8026, 0, "Identification of pieces of a trade item (ITIP) contained in a logistic unit", "N4+N14+N2+N2" ),
            new Gs1AppCode( "8110", 0, "Coupon code identification for use in North America", "N4+X..70" ),
            new Gs1AppCode( "8111", 0, "Loyalty points of a coupon", "N4+N4" ),
            new Gs1AppCode( "8112", 0, "Paperless coupon code identification for use in North America", "N4+X..70" ),
            new Gs1AppCode( "8200", 0, "Extended Packaging URL", "N4+X..70" )
        };
        #endregion data

        public override BarcodeTypes BarcodeType => BarcodeTypes.Gs1_128;

        public override int[] GetCodes(string barcode)
        {
            var segments = string.Empty;

            foreach (var item in GetPairs(barcode))
            {
                var appCode = codes.SingleOrDefault(x => x.Id >= item.App && x.Id + x.AdditionalCodes <= item.App);

                if (appCode == null)
                {
                    throw new Exception("Cannot find GS1 app code: " + item.App);
                }

                segments += appCode.ToGs1Segment(item.Val);
            }

            // no need for last separator
            if (segments.Last() == '|')
            {
                segments = segments.TrimEnd('|');
            }

            var result = new List<int>();

            // for simplicity we compres only when all data are numeric
            // todo: improve

            if (IsDigitsOnly(segments))
            {
                result = Compress(segments);
            }

            result.Add(CalculateChecksum(result.ToArray()));
            result.Add((int)CodeSet.Stop);

            return result.ToArray();
        }

        private static List<int> Compress(string segmentsStr)
        {
            var lastCodeSet = 'c';
        
            var result = new List<int>
            {
                (int)CodeSet.StartCodeC,
                (int)CodeSetC.FNC1
            };

            foreach (var segment in segmentsStr.Split('|'))
            {
                if (lastCodeSet== 'b') // result.Last() == (int)CodeSetC.CodeB)
                {
                    result.Add((int)CodeSetB.CodeC);
                    lastCodeSet = 'c';
                }

                for (var i = 0; i < segment.Length; i += 2)
                {
                    if (i + 1 < segment.Length)
                    {
                        var value = $"{segment[i]}{segment[i + 1]}";
                        result.Add(int.Parse(value));
                    }
                    else
                    {
                        result.Add((int)CodeSetC.CodeB);
                        result.Add(segment[i] - 32);
                        lastCodeSet = 'b';
                    }
                }

                result.Add((int)CodeSetC.FNC1);
            }

            if (result.Last() == (int)CodeSetC.FNC1)
            {
                result = result.Take(result.Count - 1).ToList();
            }


            return result;
        }

        private static IEnumerable<Gs1Pair> GetPairs(string data)
        {
            var result = new List<Gs1Pair>();
            var items = data.Split('(').Skip(1);

            foreach (var item in items)
            {
                var appCodeArr = item.Split(')');
                if (appCodeArr.Count() != 2)
                {
                    throw new Exception("Invalid barcode: " + data);
                }

                yield return new Gs1Pair
                {
                    App = int.Parse(appCodeArr[0]),
                    Val = appCodeArr[1]
                };
            }
        }

        private class Gs1Pair
        {
            public int App { get; set; }

            public string Val { get; set; }
        }
    }
}
