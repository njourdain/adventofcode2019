using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace part1
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var moduleMasses = await File.ReadAllLinesAsync("input.csv");

            var sum = moduleMasses.Sum(mass => CalculateFuelRequirement(int.Parse(mass)));

            Console.WriteLine($"Sum is {sum}");
        }

        public static int CalculateFuelRequirement(int mass) => mass / 3 - 2;
    }
}
