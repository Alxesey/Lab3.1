using System;
using System.Diagnostics;

namespace Lab3._1
    {
    internal static class Fermat
        {
        internal static (string, long) Factorise(int number)
            {
            // I know this ain't the best way of measuring execution time
            // and I could use benchmark or something like that. But first -
            // banchmark aren't working with Xamarin.Android for some stupid
            // reasons, just don't ask me, second - who cares, I don't care
            // so lets just do this.
            var watch = Stopwatch.StartNew();
            
            if (number <= 0)
                {
                watch.Stop();
                return ("Number cannot be below zero.", watch.ElapsedMilliseconds);
                }

            if ((number & 1) == 0)
                {
                watch.Stop();
                return ($"[ {number / 2.0}, 2 ]", watch.ElapsedMilliseconds);
                }

            int a = (int)Math.Ceiling(Math.Sqrt(number));

            if (a * a == number)
                {
                watch.Stop();
                return ($"[ {a}, {a} ]", watch.ElapsedMilliseconds);
                }

            int b = default;
            while (true)
                {
                int b1 = a * a - number;
                b = (int)(Math.Sqrt(b1));

                if (b * b == b1)
                    break;
                else
                    a += 1;
                }
            
            watch.Stop();
            return ($"[ {a - b}, {a + b} ]", watch.ElapsedMilliseconds);
            }
        }
    }