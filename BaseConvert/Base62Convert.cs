using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace BaseConvert
{
    class Base62
    {
        // Use BigInteger to prevent overflow
        static void Main(string[] args)
        {
            Console.WriteLine("Base 62 Value: LpuPe81bc2w");
            Console.WriteLine();

            Console.WriteLine("Base 62 to Decimal Conversions");
            Console.WriteLine(ConvertToDec("LpuPe81bc2w"));

            Console.WriteLine();

            Console.WriteLine("Decimal to Base 62 Conversions (General Conversion Method)");
            Console.WriteLine(Convert(ConvertToDec("LpuPe81bc2w"), 62));

            Console.WriteLine();

            Console.WriteLine("Base 62 to Decimal Conversions");
            Console.WriteLine(ConvertToDec(Convert(ConvertToDec("LpuPe81bc2w"), 62)));

            Console.WriteLine();

            Console.WriteLine("Decimal to Base 62 Conversions (Base 62 Conversion Method)");
            Console.WriteLine(Convert62(ConvertToDec("LpuPe81bc2w")));

            Console.WriteLine();

            Console.WriteLine("Base 62 to Decimal Conversions");
            Console.WriteLine(ConvertToDec(Convert62(ConvertToDec("LpuPe81bc2w"))));
        }

        static BigInteger ConvertToDec(string numText)
        {
            const string base62Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            const int base62 = 62;
            BigInteger decValue = new BigInteger(0);
            for(int index = 0; index < numText.Length; index++)
            {
                decValue += (BigInteger) (Math.Pow(base62, index)) * base62Digits.IndexOf(numText[numText.Length - 1 - index]);
            }
            return decValue;
        }

        // Assumes number >= 0 and base > 0; Does no validation of input
        public static string Convert(BigInteger number, int baseValue)
        {
            // Assume positive numbers.
            if ((number < 0) || (baseValue < 0))
                return "";
            StringBuilder bitString = new StringBuilder(); // optional to pass in capacity.. just being efficient to prevent reallocations
            while (number > 0)
            {
                int lsb = (int) (number % baseValue);  // lsb will have values from 0 to (baseValue - 1)

                bitString.Insert(0, GetChar(lsb));

                number /= baseValue;   // number = number / baseValue;
            }

            return bitString.ToString();
        }

        private static char GetChar(int number)
        {
            char ch = '0';
            if (number <= 9)
                ch = (char)(((int)'0') + number);
            else if (number <= 36)
                ch = (char)(((int)'A') + (number - 10));
            else
                ch = (char)(((int)'a') + (number - 36));

            return ch;
        }

        public static string Convert62(BigInteger number)
        {
            // Assume positive numbers.
            if (number < 0) return "";
            const int baseValue = 62;
            StringBuilder bitString = new StringBuilder(); // optional to pass in capacity.. just being efficient to prevent reallocations
            while (number > 0)
            {
                int lsb = (int)(number % baseValue);  // lsb will have values from 0 to (baseValue - 1)

                bitString.Insert(0, GetChar62(lsb));

                number /= baseValue;   // number = number / baseValue;
            }

            return bitString.ToString();
        }
        private static char GetChar62(int index)
        {
            const string base62Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return (char) base62Digits[index];
        }
    }
}
