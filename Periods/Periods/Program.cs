using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periods
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var i = 1; i < 20; i++)
            {
                getPeriod(1, i);

                Debug.WriteLine("");
            }

            //getPeriod(1, 16);

            Console.ReadLine();
        }

        static String getPeriod(Int32 dividend, Int32 divisor, UInt16 maxPerionLength = 10, UInt16 maxDecimalPartLength = 20)
        {
            Debug.WriteLine(dividend + "/" + divisor + "=" + (Double)dividend / divisor);
            Debug.Write(dividend + "/" + divisor + "=");

            var integer = 0;

            var isInteger = true;
            
            var decimals = new Int32[maxDecimalPartLength];

            var decimalLength = -1;

            while (dividend > 0 && maxDecimalPartLength-- > 0)
            {
                var remainder = 0;

                if (dividend < divisor)
                {
                    dividend *= 10;

                    //Debug.Write(0);
                }
                else
                {
                    remainder = dividend / divisor;

                    //Debug.Write(remainder);

                    dividend -= remainder * divisor;

                    dividend *= 10;
                }

                if (isInteger)
                {
                    integer = remainder;

                    isInteger = false;
                }
                else
                {
                    decimals[++decimalLength] = remainder;
                }
            }

            Debug.Write(integer);

            if (decimalLength >= 0)
            {
                Debug.Write('.');

                for (var i = 0; i <= decimalLength; i++)
                {
                    Debug.Write(decimals[i]);
                }
            }

            return "";
        }
    }
}
