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

            while (maxDecimalPartLength-- > 0)
            {
                if (dividend == 0)
                {
                    break;
                }
                if (dividend < divisor)
                {
                    dividend *= 10;

                    Debug.Write(0);
                }
                else
                {
                    var remainder = dividend / divisor;

                    Debug.Write(remainder);

                    dividend -= remainder * divisor;

                    dividend *= 10;
                }
            }

            return "";
        }
    }
}
