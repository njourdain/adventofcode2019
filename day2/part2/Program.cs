using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace part2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var fileContent = await File.ReadAllLinesAsync("input.csv");
            var program = fileContent[0].Split(",").Select(Int32.Parse).ToArray();

            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    if (part1.Program.Run(program.ToArray(), noun, verb) == 19690720)
                    {
                        Console.WriteLine($"Result is {100 * noun + verb}");
                    }
                }
            }

        }
    }
}
