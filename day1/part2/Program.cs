using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace part2
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var moduleMasses = await File.ReadAllLinesAsync("input.csv");

            var sum = moduleMasses.Sum(mass => CalculateTotalFuelRequirement(int.Parse(mass)));

            Console.WriteLine($"Sum is {sum}");
        }

        public static int CalculateTotalFuelRequirement(int mass)
        {
            var totalFuelRequirement = part1.Program.CalculateFuelRequirement(mass);
            var additionalFuel = totalFuelRequirement;

            while (additionalFuel > 0)
            {
                additionalFuel = Math.Max(0, part1.Program.CalculateFuelRequirement(additionalFuel));
                totalFuelRequirement += additionalFuel;
            }

            return totalFuelRequirement;
        }
    }
}
