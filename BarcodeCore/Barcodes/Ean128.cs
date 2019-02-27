using System;
using System.Collections.Generic;

namespace BarcodeCore.Barcodes
{
    public class Ean128 : IBarcode
    {
        #region Code patterns

        private readonly int[,] cPatterns =
        {
            {2,1,2,2,2,2,0,0},  // 0
            {2,2,2,1,2,2,0,0},  // 1
            {2,2,2,2,2,1,0,0},  // 2
            {1,2,1,2,2,3,0,0},  // 3
            {1,2,1,3,2,2,0,0},  // 4
            {1,3,1,2,2,2,0,0},  // 5
            {1,2,2,2,1,3,0,0},  // 6
            {1,2,2,3,1,2,0,0},  // 7
            {1,3,2,2,1,2,0,0},  // 8
            {2,2,1,2,1,3,0,0},  // 9
            {2,2,1,3,1,2,0,0},  // 10
            {2,3,1,2,1,2,0,0},  // 11
            {1,1,2,2,3,2,0,0},  // 12
            {1,2,2,1,3,2,0,0},  // 13
            {1,2,2,2,3,1,0,0},  // 14
            {1,1,3,2,2,2,0,0},  // 15
            {1,2,3,1,2,2,0,0},  // 16
            {1,2,3,2,2,1,0,0},  // 17
            {2,2,3,2,1,1,0,0},  // 18
            {2,2,1,1,3,2,0,0},  // 19
            {2,2,1,2,3,1,0,0},  // 20
            {2,1,3,2,1,2,0,0},  // 21
            {2,2,3,1,1,2,0,0},  // 22
            {3,1,2,1,3,1,0,0},  // 23
            {3,1,1,2,2,2,0,0},  // 24
            {3,2,1,1,2,2,0,0},  // 25
            {3,2,1,2,2,1,0,0},  // 26
            {3,1,2,2,1,2,0,0},  // 27
            {3,2,2,1,1,2,0,0},  // 28
            {3,2,2,2,1,1,0,0},  // 29
            {2,1,2,1,2,3,0,0},  // 30
            {2,1,2,3,2,1,0,0},  // 31
            {2,3,2,1,2,1,0,0},  // 32
            {1,1,1,3,2,3,0,0},  // 33
            {1,3,1,1,2,3,0,0},  // 34
            {1,3,1,3,2,1,0,0},  // 35
            {1,1,2,3,1,3,0,0},  // 36
            {1,3,2,1,1,3,0,0},  // 37
            {1,3,2,3,1,1,0,0},  // 38
            {2,1,1,3,1,3,0,0},  // 39
            {2,3,1,1,1,3,0,0},  // 40
            {2,3,1,3,1,1,0,0},  // 41
            {1,1,2,1,3,3,0,0},  // 42
            {1,1,2,3,3,1,0,0},  // 43
            {1,3,2,1,3,1,0,0},  // 44
            {1,1,3,1,2,3,0,0},  // 45
            {1,1,3,3,2,1,0,0},  // 46
            {1,3,3,1,2,1,0,0},  // 47
            {3,1,3,1,2,1,0,0},  // 48
            {2,1,1,3,3,1,0,0},  // 49
            {2,3,1,1,3,1,0,0},  // 50
            {2,1,3,1,1,3,0,0},  // 51
            {2,1,3,3,1,1,0,0},  // 52
            {2,1,3,1,3,1,0,0},  // 53
            {3,1,1,1,2,3,0,0},  // 54
            {3,1,1,3,2,1,0,0},  // 55
            {3,3,1,1,2,1,0,0},  // 56
            {3,1,2,1,1,3,0,0},  // 57
            {3,1,2,3,1,1,0,0},  // 58
            {3,3,2,1,1,1,0,0},  // 59
            {3,1,4,1,1,1,0,0},  // 60
            {2,2,1,4,1,1,0,0},  // 61
            {4,3,1,1,1,1,0,0},  // 62
            {1,1,1,2,2,4,0,0},  // 63
            {1,1,1,4,2,2,0,0},  // 64
            {1,2,1,1,2,4,0,0},  // 65
            {1,2,1,4,2,1,0,0},  // 66
            {1,4,1,1,2,2,0,0},  // 67
            {1,4,1,2,2,1,0,0},  // 68
            {1,1,2,2,1,4,0,0},  // 69
            {1,1,2,4,1,2,0,0},  // 70
            {1,2,2,1,1,4,0,0},  // 71
            {1,2,2,4,1,1,0,0},  // 72
            {1,4,2,1,1,2,0,0},  // 73
            {1,4,2,2,1,1,0,0},  // 74
            {2,4,1,2,1,1,0,0},  // 75
            {2,2,1,1,1,4,0,0},  // 76
            {4,1,3,1,1,1,0,0},  // 77
            {2,4,1,1,1,2,0,0},  // 78
            {1,3,4,1,1,1,0,0},  // 79
            {1,1,1,2,4,2,0,0},  // 80
            {1,2,1,1,4,2,0,0},  // 81
            {1,2,1,2,4,1,0,0},  // 82
            {1,1,4,2,1,2,0,0},  // 83
            {1,2,4,1,1,2,0,0},  // 84
            {1,2,4,2,1,1,0,0},  // 85
            {4,1,1,2,1,2,0,0},  // 86
            {4,2,1,1,1,2,0,0},  // 87
            {4,2,1,2,1,1,0,0},  // 88
            {2,1,2,1,4,1,0,0},  // 89
            {2,1,4,1,2,1,0,0},  // 90
            {4,1,2,1,2,1,0,0},  // 91
            {1,1,1,1,4,3,0,0},  // 92
            {1,1,1,3,4,1,0,0},  // 93
            {1,3,1,1,4,1,0,0},  // 94
            {1,1,4,1,1,3,0,0},  // 95
            {1,1,4,3,1,1,0,0},  // 96
            {4,1,1,1,1,3,0,0},  // 97
            {4,1,1,3,1,1,0,0},  // 98
            {1,1,3,1,4,1,0,0},  // 99
            {1,1,4,1,3,1,0,0},  // 100
            {3,1,1,1,4,1,0,0},  // 101
            {4,1,1,1,3,1,0,0},  // 102
            {2,1,1,4,1,2,0,0},  // 103
            {2,1,1,2,1,4,0,0},  // 104
            {2,1,1,2,3,2,0,0},  // 105
            {2,3,3,1,1,1,2,0}   // 106
        };

        #endregion Code patterns

        public virtual BarcodeTypes BarcodeType => BarcodeTypes.Ean_128;

        public Action<BarcodeBar> OnRenderBar { get; set; }

        public virtual void Render(string barcode)
        {
            var codes = this.GetCodes(barcode);

            const int BarWeight = 1;
            var width = ((codes.Length - 3) * 11 + 35) * BarWeight;

            int cursor = 0;

            for (int codeidx = 0; codeidx < codes.Length; codeidx++)
            {
                int code = codes[codeidx];

                for (int bar = 0; bar < 8; bar += 2)
                {
                    int barwidth = cPatterns[code, bar] * BarWeight;
                    int spcwidth = cPatterns[code, bar + 1] * BarWeight;

                    if (barwidth > 0)
                    {
                        this.OnRenderBar(new BarcodeBar { X = cursor, Width = barwidth });
                    }

                    cursor += (barwidth + spcwidth);
                }
            }
        }

        public virtual int[] GetCodes(string barcode)
        {
            // for simplicity, ony CodeSet B is currently implemented
            var result = new List<int> { (int)CodeSet.StartCodeB };

            foreach (var character in barcode)
            {
                result.Add(character - 32);
            }

            result.Add(CalculateChecksum(result.ToArray()));
            result.Add((int)CodeSet.Stop);

            return result.ToArray();
        }

        protected static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        protected static int CalculateChecksum(int[] values)
        {
            int sum = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                sum += (i * values[i]);
            }
            return sum % 103;
        }

        protected enum CodeSetC
        {
            CodeB = 100,
            CodeA = 101,
            FNC1 = 102
        }

        protected enum CodeSetB
        {
            FNC3 = 96,
            FNC2 = 97,
            ShiftA = 98,
            CodeC = 99,
            FNC4 = 100,
            CodeA = 101,
            FNC1 = 102
        }

        protected enum CodeSetA
        {
            FNC3 = 96,
            FNC2 = 97,
            ShiftB = 98,
            CodeC = 99,
            CodeB = 100,
            FNC4 = 101,
            FNC1 = 102
        }

        protected enum CodeSet
        {
            StartCodeA = 103,
            StartCodeB = 104,
            StartCodeC = 105,
            Stop = 106
        }
    }
}
