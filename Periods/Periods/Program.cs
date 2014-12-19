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
            test(1, 9);
            test(1, 3);
            test(9, 110);
            test(7, 12);
            test(1, 81);
            test(22, 7);
            test(1, 17);
            test(1, 19);
            test(1, 23);
            test(1, 29);
            test(1, 97);
            test(1, 970); //!!!

            Console.ReadLine();
        }

        static void test(Int32 dividend, Int32 divisor)
        {
            Console.WriteLine(dividend + "/" + divisor + "=" + (Double)dividend / divisor);

            Console.WriteLine(dividend + "/" + divisor + "=" + getPeriod(dividend, divisor, 200));

            Console.WriteLine();
        }

        static String getPeriod(Int32 dividend, Int32 divisor, Int16 maxLength = 7)
        {
            var length = maxLength;

            var decimalLength = -1;

            var integer = 0;

            var isInteger = true;

            var decimals = new Int32[maxLength];

            var digitIndexes1 = new[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

            var digitIndexes2 = new[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

            var periodStartIndex = -1;

            var periodEndIndex = -1;

            while (dividend > 0 && length-- >= 0 && periodStartIndex < 0 && periodEndIndex < 0)
            {
                var remainder = 0;

                if (dividend >= divisor)
                {
                    remainder = dividend / divisor;

                    dividend -= remainder * divisor;
                }

                dividend *= 10;

                if (isInteger)
                {
                    integer = remainder;

                    isInteger = false;
                }
                else
                {
                    ++decimalLength;

                    decimals[decimalLength] = remainder;

                    if (digitIndexes1[remainder] >= 0 && digitIndexes2[remainder] >= 0)
                    {
                        var periodLendth = 0;

                        while (decimalLength / 2 > periodLendth)
                        {
                            while (decimalLength / 2 > periodLendth)
                            {
                                if (decimals[periodLendth++] == remainder)
                                {
                                    break;
                                }
                            }

                            var equal = true;

                            for (var index = 0; index < periodLendth && equal; index++)
                            {
                                if (decimals[decimalLength - index] != decimals[decimalLength - periodLendth - index])
                                {
                                    equal = false;
                                }
                            }

                            if (equal)
                            {
                                periodEndIndex = decimalLength - periodLendth;

                                periodStartIndex = periodEndIndex - periodLendth;

                                periodEndIndex--;
                            }
                        }
                    }

                    digitIndexes2[remainder] = digitIndexes1[remainder];

                    digitIndexes1[remainder] = decimalLength;
                }
            }

            if (decimalLength >= 0)
            {
                var builder = new StringBuilder(decimalLength);

                builder.Append(integer);

                builder.Append('.');

                for (var index = 0; index <= decimalLength; index++)
                {
                    if (index == periodStartIndex)
                    {
                        builder.Append('(');
                    }

                    builder.Append(decimals[index]);

                    if (index == periodEndIndex)
                    {
                        builder.Append(')');

                        break;
                    }
                }

                return builder.ToString();
            }

            return integer.ToString();
        }
    }
}
