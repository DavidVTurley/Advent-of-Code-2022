namespace Advent_of_Code.Days;

using Extras;

public class Day9 : IDay
{
    public Object Setup(HttpClient client)
    {
        Input = ExtraFunctions.MakeAdventOfCodeInputRequest(client, 9).Split('\n', StringSplitOptions.RemoveEmptyEntries);

        //Input = "R 4\nU 4\nL 3\nD 1\nR 4\nD 1\nL 5\nR 2\n".Split('\n', StringSplitOptions.RemoveEmptyEntries);
        return Input;
    }

    public String[] Input = null!;

    public void Challenge1(Object input)
    {
        List<Vec2> knotList = new List<Vec2>()
        {
            new Vec2(0, 0),
            new Vec2(0, 0),
        };

        Dictionary<Vec2, Int32> positionsVisited = SimulateRope(Input, knotList);
        
    }

    private static Dictionary<Vec2, Int32> SimulateRope(IEnumerable<String> commands, IList<Vec2> knotList)
    {
        Decimal maxX = 0;
        Decimal maxY = 0;
        Dictionary<Vec2, Int32> positionsVisited = new() { { new Vec2(0, 0), 1 } };
        foreach (String command in commands)
        {
            for (Int32 i = 0; i < Int32.Parse(command.Substring(2)); i++)
            {
                Direction direction = (Direction)command[0];
                knotList[0] += Vec2.GetDirectionVector(direction);
                
                for (Int32 knotIndex = 1; knotIndex < knotList.Count; knotIndex++)
                {
                    Boolean tailMoved = knotList[knotIndex].TargetWithinNUnits(knotList[knotIndex-1], 1);
                    if (!tailMoved) continue;

                    Vec2 vecToTarget = knotList[knotIndex].GetVectorToTarget(knotList[knotIndex-1]).Normalize();

                    if (vecToTarget.X is 1 or -1)
                    {
                        Vec2 add = new Vec2(vecToTarget.X, 0);
                        if (vecToTarget.Y != 0)
                            add.Y += vecToTarget.Y > 0 ? Math.Ceiling(vecToTarget.Y) : Math.Floor(vecToTarget.Y);
                        knotList[knotIndex] += add;
                    }
                    else if (vecToTarget.Y is 1 or -1)
                    {
                        Vec2 add = new Vec2(0, vecToTarget.Y);
                        if (vecToTarget.X != 0)
                            add.X += vecToTarget.X > 0 ? Math.Ceiling(vecToTarget.X) : Math.Floor(vecToTarget.X);
                        knotList[knotIndex] += add;
                    }
                }
                if (!positionsVisited.TryAdd(knotList[^1], 1))
                {
                    positionsVisited[knotList[^1]]++;
                }
            }
        }

        return positionsVisited;
    }

    public void Challenge2(Object input)
    {
        //Input = "R 5\nU 8\nL 8\nD 3\nR 17\nD 10\nL 25\nU 20".Split('\n', StringSplitOptions.RemoveEmptyEntries);

        List<Vec2> knotList = new List<Vec2>(10);
        for (Int32 i = 0; i < 10; i++)
        {
            knotList.Add(new Vec2(0, 0));
        }

        Dictionary<Vec2, Int32> positionVisited = SimulateRope(Input, knotList);
        
    }
}



////public class Day9 : IDay
////{

////    private List<(Direction direction, Int32 move)> steps = new();

////    public async Task Setup(HttpClient client)
////    {
////        String input = await ExtraFunctions.MakeAdventOfCodeInputRequest(client, 9);

////        input = "R 4\nU 4\nL 3\nD 1\nR 4\nD 1\nL 5\nR 2\n";

////        steps = new List<(Direction, Int32)>(input.Length / 4);


////        foreach (String s in input.Split('\n', StringSplitOptions.RemoveEmptyEntries))
////        {
////            steps.Add((
////                    (Direction)s[0],
////                    Int32.Parse(s[2].ToString(CultureInfo.InvariantCulture)))
////                );
////        }

////    }

////    public void Challenge1()
////    {
////        Dictionary<Point, Int32> tailPositionsVisited = new()
////        {
////            {new Point(0,0), 1}
////        }; ;

////        Rope rope = new Rope(0, 0);
////        Rope tail = new Rope(0, 0);

////        foreach ((Direction direction, Int32 move) in steps)
////        {
////            for (Int32 i = 0; i < move; i++)
////            {
////                rope.Move(direction);
////                Point p = tail.FollowMove(direction, rope);
////                if(p == tail.PreviousPoint) continue;

////                PaintRopeAndTail(rope, tail);

////                if (!tail.HasMoved) continue;

////                if (tailPositionsVisited.ContainsKey(p))
////                {
////                    tailPositionsVisited[p]++;

////                }
////                else
////                {
////                    tailPositionsVisited.Add(p, 1);
////                }
////                tail.HasMoved = false;
////            }
////        }

////        var s = tailPositionsVisited.Count;

////        List<List<Boolean>> c = new List<List<Boolean>>().AddRange(new List<Boolean>[]);
////        foreach (KeyValuePair<Point, Int32> keyValuePair in tailPositionsVisited)
////        {
////            ;
////        }
////    }

////    public void Challenge2()
////    {
////        throw new NotImplementedException();
////    }

////    public void PaintRopeAndTail(Rope rope, Rope tail){



////         I know this is horribly inificient, but it's late and I want to go to bed.


////        Int32 xOffset = tail.CurrentPoint.X - rope.CurrentPoint.X;
////        Int32 yOffset = tail.CurrentPoint.Y - rope.CurrentPoint.Y;

////        List<List<Char>> sa = new()
////        {
////            new List<Char>(3){'x', 'x', 'x'},
////            new List<Char>(3){'x', 'S', 'x'},
////            new List<Char>(3){'x', 'x', 'x'},
////        };

////        sa[yOffset + 1][xOffset + 1] = 'T';

////        foreach (List<Char> chars in sa)
////        {
////            foreach (Char c in chars)
////            {
////                Console.Write(c);
////            }

////            Console.WriteLine();
////        }

////        Console.WriteLine("---------------------------------------------");

////    }

////     So turns out records are a thing
////    public record Point
////    {
////        public Int32 X;
////        public Int32 Y;

////        public Point(Int32 x, Int32 y)
////        {
////            X = x;
////            Y = y;
////        }



////        public static Boolean operator ==(Point a, Point b)
////        {
////            return a.X == b.X && a.Y == b.Y;
////        }
////        public static Boolean operator !=(Point a, Point b)
////        {
////            return !(a == b);
////        }

////        public override Boolean Equals(Object? obj)
////        {
////            return obj is Point other && Equals(other);
////        }

////        public Boolean Equals(Point other)
////        {
////            return X == other.X && Y == other.Y;
////        }

////        public override Int32 GetHashCode()
////        {
////            return HashCode.Combine(X, Y);
////        }

////        public override String ToString()
////        {
////            return $"X:{X}, Y:{Y}";
////        }

////        public Object Clone()
////        {
////            return new Point(X, Y);
////        }
////    }

////    public struct Rope
////    {
////        public Point PreviousPoint;
////        public Point CurrentPoint;
////        public Boolean HasMoved = false;

////        public Rope(Int32 x, Int32 y)
////        {
////            PreviousPoint = new Point(x, y);
////            CurrentPoint = new Point(x, y);
////        }


////        / <summary>
////        / Moves all directions indicated, and only after updates the previous Point
////        / </summary>
////        / <param name="directions"></param>
////        / <returns></returns>
////        public Point Move(Direction[] directions)
////        {
////            Point p = (Point) CurrentPoint.Clone();

////            foreach (Direction direction in directions)
////            {
////                Move(direction);
////            }

////            PreviousPoint = p;

////            return CurrentPoint;
////        }
////        public Point Move(Direction direction)
////        {
////            PreviousPoint = (Point) CurrentPoint;
////            switch (direction)
////            {
////                case Direction.Down:
////                    CurrentPoint.Y--;
////                    break;
////                case Direction.Left:
////                    CurrentPoint.X--;
////                    break;
////                case Direction.Right:
////                    CurrentPoint.X++;
////                    break;
////                case Direction.Up:
////                    CurrentPoint.Y++;
////                    break;
////                default:
////                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
////            }

////            HasMoved = true;
////            return CurrentPoint;
////        }

////        public Point Move(Point point)
////        {
////            PreviousPoint = (Point)CurrentPoint.Clone();
////            CurrentPoint = (Point)point.Clone();
////            HasMoved = true;
////            return (Point)CurrentPoint.Clone();
////        }

////        public Point FollowMove(Direction direction, Rope toFollow)
////        {
////            if (CurrentPoint == toFollow.PreviousPoint) return CurrentPoint;

////            Int32 oppositeAxisMove = 0;

////            switch (direction)
////            {
////                case Direction.Down:

////                    if (toFollow.CurrentPoint.Y - CurrentPoint.Y != -2) return CurrentPoint;

////                    oppositeAxisMove = toFollow.CurrentPoint.X - CurrentPoint.X;
////                    switch (oppositeAxisMove)
////                    {
////                        case > 0:
////                            Move(new[] { Direction.Right, Direction.Down });
////                            break;
////                        case 0:
////                            Move(Direction.Down);
////                            break;
////                        case < 0:
////                            Move(new[] { Direction.Left, Direction.Down });
////                            break;
////                    }

////                    break;
////                case Direction.Up:
////                    if (toFollow.CurrentPoint.Y - CurrentPoint.Y != 2) return CurrentPoint;

////                    oppositeAxisMove = toFollow.CurrentPoint.X - CurrentPoint.X;
////                    switch (oppositeAxisMove)
////                    {
////                        case > 0:
////                            Move(new[] { Direction.Right, Direction.Up });
////                            break;
////                        case 0:
////                            Move(Direction.Up);
////                            break;
////                        case < 0:
////                            Move(new[] { Direction.Left, Direction.Up });
////                            break;
////                    }

////                    break;

////                case Direction.Left:
////                    if (toFollow.CurrentPoint.X - CurrentPoint.X != -2) return CurrentPoint;

////                    oppositeAxisMove = toFollow.CurrentPoint.Y - CurrentPoint.Y;
////                    switch (oppositeAxisMove)
////                    {
////                        case > 0:
////                            Move(new[] { Direction.Left, Direction.Up });
////                            break;
////                        case 0:
////                            Move(Direction.Left);
////                            break;
////                        case < 0:
////                            Move(new[] { Direction.Left, Direction.Down });
////                            break;
////                    }

////                    break;
////                case Direction.Right:
////                    if (toFollow.CurrentPoint.X - CurrentPoint.X != 2) return CurrentPoint;

////                    oppositeAxisMove = toFollow.CurrentPoint.Y - CurrentPoint.Y;
////                    switch (oppositeAxisMove)
////                    {
////                        case 1:
////                            Move(new[] { Direction.Right, Direction.Up });
////                            break;
////                        case 0:
////                            Move(Direction.Right);
////                            break;
////                        case -1:
////                            Move(new[] { Direction.Right, Direction.Down });
////                            break;
////                    }

////                    break;
////                default:
////                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
////            }

////            HasMoved = true;
////            return CurrentPoint;
////        }
////    }
////}