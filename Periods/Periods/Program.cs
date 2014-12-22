﻿using System;
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
            test(7, 12); //!!!
            test(1, 81);
            test(22, 7);
            test(1, 17);
            test(1, 19); //???
            test(1, 23); //???
            test(1, 29); //???
            test(1, 97); //???
            test(1, 970); //!!!

            Console.ReadLine();
        }

        static void test(Int32 dividend, Int32 divisor)
        {
            Console.WriteLine(dividend + "/" + divisor + "=" + (Double)dividend / divisor);

            Console.WriteLine(dividend + "/" + divisor + "=" + getPeriod(dividend, divisor, 200));

            Console.WriteLine();
        }

        static String getPeriod(Int32 dividend, Int32 divisor, Int16 maxLength = 200)
        {
            var index = -1; // индекс текущего демятичного знака

            var integer = 0; // целая часть деления

            var isInteger = true; // флаг указывает, что идут вычисления целых частей

            var decimals = new Int32[maxLength]; // цифры после запятой

            var endIndexes = new[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }; // предпоследние индексы цифр 0-9

            var preEndIndexes = new[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }; // последние индексы цифр 0-9

            var periodStartIndex = -1; // индекс начала периода

            var periodEndIndex = -1; // индекс окончания периода

            while (dividend > 0 && index <= maxLength && periodStartIndex < 0 && periodEndIndex < 0)
            {
                var remainder = 0;

                // получение десятичных знаков
                {
                    if (dividend >= divisor)
                    {
                        remainder = dividend / divisor;

                        dividend -= remainder * divisor;
                    }

                    dividend *= 10;
                }

                if (isInteger)
                {
                    integer = remainder;

                    isInteger = false;
                }
                else
                {
                    ++index;

                    decimals[index] = remainder;

                    if (endIndexes[remainder] >= 0 && preEndIndexes[remainder] >= 0) // признак наличия периода
                    {
                        var periodLendth = 0;

                        while (index / 2 > periodLendth) // поиск периодов
                        {
                            while (index / 2 > periodLendth) // поиск периода
                            {
                                if (decimals[periodLendth++] == remainder)
                                {
                                    break;
                                }
                            }

                            var isEqual = true;

                            for (var i = 0; i < periodLendth && isEqual; i++) // проверка периода
                            {
                                if (decimals[index - i] != decimals[index - periodLendth - i])
                                {
                                    isEqual = false;
                                }
                            }

                            if (isEqual) //вычисление размеров периода
                            {
                                periodEndIndex = index - periodLendth;

                                periodStartIndex = periodEndIndex - periodLendth;

                                periodEndIndex--;
                            }
                        }
                    }

                    preEndIndexes[remainder] = endIndexes[remainder];

                    endIndexes[remainder] = index;
                }
            }

            if (index >= 0) // формирование строки вывода
            {
                var builder = new StringBuilder(index);

                builder.Append(integer);

                builder.Append('.');

                for (var i = 0; i <= periodEndIndex; i++)
                {
                    if (i == periodStartIndex)
                    {
                        builder.Append('(');
                    }

                    builder.Append(decimals[i]);

                    if (i == periodEndIndex)
                    {
                        builder.Append(')');
                    }
                }

                return builder.ToString();
            }

            return integer.ToString();
        }
    }
}
