using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using part1;

namespace part2
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var cableMovesAsStrings = await File.ReadAllLinesAsync("input.txt");

            var fewestSteps = GetFewestCombinedStepsToIntersection(cableMovesAsStrings);

            Console.WriteLine($"Fewest combined steps to intersection is {fewestSteps}");
        }

        public static int GetFewestCombinedStepsToIntersection(string[] cableMovesAsStrings)
        {
            var cables = cableMovesAsStrings.Select(
                    cableMovesAsString => cableMovesAsString
                        .Split(',')
                        .Select(moveAsString => new Move(moveAsString))
                )
                .Select(moves => part1.Program.ConvertMovesToSegments(moves).ToArray())
                .ToArray();

            var intersections = cables[0]
                .SelectMany(segment => cables[1].Select(segment.GetIntersectionPoint))
                .Where(intersection => intersection != null && !intersection.Equals(part1.Program.Origin))
                .Select(intersection => intersection.Value);

            return intersections
                .Min(intersection => GetCableLengthToIntersection(cables[0], intersection) + GetCableLengthToIntersection(cables[1], intersection));
        }

        private static int GetCableLengthToIntersection(Segment[] cable, Point intersection)
        {
            var length = 0;
            foreach (var segment in cable)
            {
                length += segment.Length;
                // length -= cable.TakeWhile(s => s != segment).Sum(s => s.GetOverlap(segment));

                if (segment.Contains(intersection))
                {
                    length -= segment.GetLengthToPoint2(intersection);
                    return length;
                }
            }

            throw new Exception("Intersection should always cross cable");
        }
    }
}
