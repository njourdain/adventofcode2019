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
            var fileContent = await File.ReadAllLinesAsync("input.csv");
            var program = fileContent[0].Split(",").Select(Int32.Parse).ToArray();

            var result = Run(program, 12, 2);

            Console.WriteLine($"Position 0 has value {program[0]}");
        }

        public static int Run(int[] program, int noun, int verb)
        {
            program[1] = noun;
            program[2] = verb;

            for (var i = 0; i < program.Length; i += 4)
            {
                if (program[i] == 1)
                {
                    program[program[i + 3]] = program[program[i + 1]] + program[program[i + 2]];
                }
                else if (program[i] == 2)
                {
                    program[program[i + 3]] = program[program[i + 1]] * program[program[i + 2]];
                }
                else if (program[i] == 99)
                {
                    break;
                }
            }

            return program[0];
        }
    }
}
