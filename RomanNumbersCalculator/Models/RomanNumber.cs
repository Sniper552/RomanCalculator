using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumbersCalculator.Models
{
    internal class RomanNumber: IComparable, ICloneable
    {
        private ushort number = 1;

        private string romanNumber = "";
        public RomanNumber(ushort number)
        {
            if (number < 1 || number > 3999) throw new RomanNumberException();

            this.number = number;

            string romanNumberst = "";
            
            string[] ones = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };
            string[] tens = { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
            string[] hundr = { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
            string[] thous = { "", "M", "MM", "MMM" };

            romanNumberst += thous[(number / 1000) % 10];
            romanNumberst += hundr[(number / 100) % 10];
            romanNumberst += tens[(number / 10) % 10];
            romanNumberst += ones[number % 10];
            romanNumber = romanNumberst;
        }

        public RomanNumber(string number){
            romanNumber = number;
            Dictionary<char, ushort> romanNumbers = new Dictionary<char, ushort>
            {
                { 'I', 1 },
                { 'V', 5 },
                { 'X', 10 },
                { 'L', 50 },
                { 'C', 100 },
                { 'D', 500 },
                { 'M', 1000 }
            };
            if (number.Length == 1) this.number = romanNumbers[number[0]];
            else
            {
                ushort NewNumber = 0, Setnumber = 0;
                while (Setnumber < number.Length - 1)
                {
                    if (romanNumbers[number[Setnumber]] < romanNumbers[number[Setnumber + 1]])
                    {
                        NewNumber += (ushort)(romanNumbers[number[Setnumber + 1]] - romanNumbers[number[Setnumber]]);
                        Setnumber += 2;
                    }
                    else
                    {
                        NewNumber += romanNumbers[number[Setnumber]];
                        Setnumber++;
                        if (Setnumber == number.Length - 1)
                            NewNumber += romanNumbers[number[Setnumber]];
                    }
                }
                this.number = NewNumber;
            }
            if (number != new RomanNumber(this.number).ToString()) throw new RomanNumberException();
            if (this.number < 1 || this.number > 3999) throw new RomanNumberException();
        }

        public static RomanNumber Add(RomanNumber RomanNumber1, RomanNumber RomanNumber2)
        {
            return new RomanNumber((ushort)(RomanNumber1.number + RomanNumber2.number));
        }
        public static RomanNumber Sub(RomanNumber RomanNumber1, RomanNumber RomanNumber2)
        {
            return new RomanNumber((ushort)(RomanNumber1.number - RomanNumber2.number));
        }
        public static RomanNumber Mul(RomanNumber RomanNumber1, RomanNumber RomanNumber2)
        {
            return new RomanNumber((ushort)(RomanNumber1.number * RomanNumber2.number));
        }
        public static RomanNumber Div(RomanNumber RomanNumber1, RomanNumber RomanNumber2)
        {
            return new RomanNumber((ushort)(RomanNumber1.number / RomanNumber2.number));

        }

        public int CompareTo(object? obj)
        {
            if (obj is RomanNumber num) return number.CompareTo(num.number);
            else throw new ArgumentException("Unable to compare this parameter.");
        }

        public object Clone() => MemberwiseClone();

        public override string ToString() => romanNumber;

        public ushort ToUInt16() => number;
    }
}
