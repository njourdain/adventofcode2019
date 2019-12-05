using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace part1
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var cableMovesAsStrings = await File.ReadAllLinesAsync("input.txt");

            var cables = cableMovesAsStrings.Select(
                cableMovesAsString => cableMovesAsString
                    .Split(',')
                    .Select(moveAsString => new Move(moveAsString))
            )
                .Select(ConvertMovesToSegments)
                .ToArray();

            var manhattanDistance = cables[0]
                .Min(
                    segment => cables[1]
                        .Select(segment.GetIntersectionPoint)
                        .Where(intersection => intersection != null && !intersection.Equals((Origin)))
                        .Select(intersection => GetManhattanDistance(intersection.Value))
                        .DefaultIfEmpty(int.MaxValue)
                        .Min()
                );

            Console.WriteLine($"Manhattan distance is {manhattanDistance}");
        }

        public static Point Origin = new Point { X = 0, Y = 0 };

        public static IEnumerable<Segment> ConvertMovesToSegments(IEnumerable<Move> moves)
        {
            var currentPoint = Origin;

            foreach (var move in moves)
            {
                var segment = new Segment(currentPoint, move);
                yield return segment;
                currentPoint = segment.Point2;
            }
        }

        private static int GetManhattanDistance(Point point) => Math.Abs(point.X) + Math.Abs(point.Y);
    }

    public class Segment
    {
        public Point Point1 { get; }
        public Point Point2 { get; }
        public bool IsVertical { get; }
        public int Length { get; }

        public Segment(Point origin, Move move)
        {
            Point1 = origin;
            IsVertical = move.Direction == Direction.Up || move.Direction == Direction.Down;
            Length = move.Amount;

            switch (move.Direction)
            {
                case Direction.Up:
                    Point2 = new Point { X = origin.X, Y = origin.Y + move.Amount };
                    break;
                case Direction.Down:
                    Point2 = new Point { X = origin.X, Y = origin.Y - move.Amount };
                    break;
                case Direction.Left:
                    Point2 = new Point { X = origin.X - move.Amount, Y = origin.Y };
                    break;
                case Direction.Right:
                    Point2 = new Point { X = origin.X + move.Amount, Y = origin.Y };
                    break;
            }
        }

        public Point? GetIntersectionPoint(Segment otherSegment)
        {
            if (IsVertical == otherSegment.IsVertical)
            {
                return null;
            }

            var vertical = IsVertical ? this : otherSegment;
            var horizontal = IsVertical ? otherSegment : this;

            return vertical.Point1.X >= Math.Min(horizontal.Point1.X, horizontal.Point2.X)
                && vertical.Point1.X <= Math.Max(horizontal.Point1.X, horizontal.Point2.X)
                && horizontal.Point1.Y >= Math.Min(vertical.Point1.Y, vertical.Point2.Y)
                && horizontal.Point1.Y <= Math.Max(vertical.Point1.Y, vertical.Point2.Y)
                ? new Point { X = vertical.Point1.X, Y = horizontal.Point1.Y }
                : (Point?)null;
        }

        public bool Contains(Point point)
            => IsVertical
                ? Point1.X == point.X && point.Y >= Math.Min(Point1.Y, Point2.Y) && point.Y <= Math.Max(Point1.Y, Point2.Y)
                : Point1.Y == point.Y && point.X >= Math.Min(Point1.X, Point2.X) && point.X <= Math.Max(Point1.X, Point2.X);

        public int GetOverlap(Segment otherSegment)
        {
            if (IsVertical != otherSegment.IsVertical)
                return 0;

            Func<Point, int> value = (Point p) => IsVertical ? p.Y : p.X;

            var segment1Left = Math.Min(value(Point1), value(Point2));
            var segment1Right = Math.Max(value(Point1), value(Point2));
            var segment2Left = Math.Min(value(otherSegment.Point1), value(otherSegment.Point2));
            var segment2Right = Math.Max(value(otherSegment.Point1), value(otherSegment.Point2));

            var biggestLeft = Math.Max(segment1Left, segment2Left);
            var smallestRight = Math.Min(segment1Right, segment2Right);

            if (smallestRight <= biggestLeft)
                return 0;

            return smallestRight - biggestLeft;
        }

        public int GetLengthToPoint2(Point point) => IsVertical ? Math.Abs(Point2.Y - point.Y) : Math.Abs(Point2.X - point.X);
    }

    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool Equals(Point point) => point.X == X && point.Y == Y;
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Move
    {
        public Direction Direction { get; }
        public int Amount { get; }

        public Move(string move)
        {
            Direction = ConvertCharToDirection(move[0]);
            Amount = int.Parse(string.Concat(move.Skip(1)));
        }

        private Direction ConvertCharToDirection(char value) =>
            value switch
            {
                'U' => Direction.Up,
                'D' => Direction.Down,
                'L' => Direction.Left,
                'R' => Direction.Right
            };
    }
}
