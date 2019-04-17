using System;
using System.Linq;

namespace BarcodeCore.Barcodes.GS1
{
    public class Gs1AppCode
    {
        public Gs1AppCode(string id, int additionalCodes, string description, string spec)
        {
            spec = spec.ToUpper();
            this.Id = int.Parse(id);
            this.IdStr = id;
            this.AdditionalCodes = additionalCodes;
            this.Description = description;
            this.IsFixed = !spec.Contains("..");

            var items = spec.Split('+');

            if (items.Count() != 2)
            {
                throw new Exception("Unsupported app code: " + this.Id);
            }

            this.AppCodeLength = int.Parse(items[0].Replace("N", string.Empty));

            if (this.AppCodeLength != id.Length)
            {
                throw new Exception($"Inconsistent app code length {id}, expected length: {this.AppCodeLength}");
            }

            this.IsNumeric = (items[1].StartsWith("N", StringComparison.InvariantCultureIgnoreCase));

            if (!this.IsFixed)
            {
                var minMax = items[1].Split(new string[] { ".." }, StringSplitOptions.None);

                minMax[0] = minMax[0].Substring(1);

                if (minMax[0] == string.Empty)
                {
                    minMax[0] = "0";
                }

                this.FlexLengthMin = int.Parse(minMax[0]);
                this.FlexLengthMax = int.Parse(minMax[1]);
            }
            else
            {
                var fv = items[1].Substring(1);
                if (fv == string.Empty)
                {
                    fv = "0";
                }

                this.FixLengthSize = int.Parse(fv);
            }
        }

        public int Id { get; }

        public string IdStr { get; }

        public int AdditionalCodes { get; }

        public string Description { get; }

        public bool IsFixed { get; }

        public bool IsNumeric { get; }

        public int FixLengthSize { get; }

        public int FlexLengthMin { get; }

        public int FlexLengthMax { get; }

        public int AppCodeLength { get; }

        public string ToGs1Segment(string value)
        {
            this.Parse(value);

            value = this.IdStr + value;

            if (!this.IsFixed)
            {
                value+='|';
            }

            return value;
        }

        private static bool IsDigitsOnly(string str)
        {
            foreach (char c in str.Replace("|",string.Empty))
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private void Parse(string value)
        {
            var appCode = $"GS1 APPCODE {this.Id} {this.AdditionalCodes}.";

            if (this.IsFixed)
            {
                if (value.Length != this.FixLengthSize)
                {
                    throw new Exception($"{appCode} Invalid length, expected {this.FixLengthSize}, actual {value.Length}");
                }
            }
            else
            {
                if (value.Length < this.FlexLengthMin || value.Length > this.FlexLengthMax)
                {
                    throw new Exception($"{appCode} Invalid length, expected between {this.FlexLengthMin} and {this.FlexLengthMax}, actual {value.Length}");
                }
            }

            if (this.IsNumeric && !IsDigitsOnly(value))
            {
                throw new Exception($"{appCode} Only digits are accepted");
            }
        }
    }
}
