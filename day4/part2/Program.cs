using System;
using System.Collections.Generic;
using System.Linq;

namespace part2
{
    class Program
    {
        static void Main(string[] args)
        {
            var count = 0;
            for (var i = 402328; i <= 864247; i++)
            {
                var number = i.ToString();
                var hasDecreasing = false;

                for (var j = 0; j < 5; j++)
                {
                    if (number[j] > number[j + 1])
                    {
                        hasDecreasing = true;
                        break;
                    }
                }

                if (hasDecreasing)
                    continue;

                if (number.GroupBy(x => x).Any(x => x.Count() == 2))
                    count++;
            }

            Console.WriteLine($"{count} possible passwords");
        }
    }
}
