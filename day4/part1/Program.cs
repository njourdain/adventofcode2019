using System;
using System.Linq;

namespace part1
{
    class Program
    {
        static void Main(string[] args)
        {
            var count = 0;
            for (var i = 402328; i <= 864247; i++)
            {
                var number = i.ToString();
                var hasDoubleDigit = false;
                var hasDecreasing = false;

                for (var j = 0; j < 5; j++)
                {
                    hasDoubleDigit |= number[j] == number[j + 1];

                    if (number[j] > number[j + 1])
                    {
                        hasDecreasing = true;
                        break;
                    }
                }

                if (hasDoubleDigit && !hasDecreasing)
                    count++;
            }

            Console.WriteLine($"{count} possible passwords");
        }
    }
}
