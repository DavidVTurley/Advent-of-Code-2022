namespace Advent_of_Code.Days;

using Extras;

public class Day9 : IDay
{
    internal record struct Vec2(Int32 X, Int32 Y)
    {
        public Int32 X { get; set; } = X;
        public Int32 Y { get; set; } = Y;

        public void Move(Char direction)
        {
            switch (direction)
            {
                case 'D':
                    Y--;
                    break;
                case 'U':
                    Y++;
                    break;
                case 'L':
                    X--;
                    break;
                case 'R':
                    X++;
                    break;
            }
        }

        public Vec2 DirectionToVec2(Vec2 toVec2)
        {
            return new Vec2(toVec2.X - X, toVec2.Y - Y);
        }
    }

    public Task Setup(HttpClient client)
    {
        Input = ExtraFunctions.MakeAdventOfCodeInputRequest(client, 9).Result.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        //Input = "R 4\nU 4\nL 3\nD 1\nR 4\nD 1\nL 5\nR 2\n".Split('\n', StringSplitOptions.RemoveEmptyEntries);

        return Task.CompletedTask;
    }

    public String[] Input = null!;

    public void Challenge1()
    {
        Dictionary<Vec2, Int32> positionsVisited = new() { { new Vec2(0, 0), 1 } };

        Vec2 rope = new Vec2(0, 0);
        Vec2 tail = new Vec2(0, 0);

        foreach (String s in Input)
        {
            Int32 count = Int32.Parse(s.Substring(2));
            for (Int32 i = 0; i < count; i++)
            {
                rope.Move(s[0]);
                Vec2 direction = tail.DirectionToVec2(rope);

                Boolean tailMoved = false;
                switch (s[0])
                {
                    case 'D' when direction.Y == -2:
                        tailMoved = true;
                        tail.Move(s[0]);
                        if (direction.X == 0) break;
                        tail.X += direction.X;
                        break;
                    case 'U' when direction.Y == 2:
                        tailMoved = true;
                        tail.Move(s[0]);
                        if (direction.X == 0) break;
                        tail.X += direction.X;
                        break;
                    case 'L' when direction.X == -2:
                        tailMoved = true;
                        tail.Move(s[0]);
                        if (direction.Y == 0) break;
                        tail.Y += direction.Y;
                        break;
                    case 'R' when direction.X == 2:
                        tailMoved = true;
                        tail.Move(s[0]);
                        if (direction.Y == 0) break;
                        tail.Y += direction.Y;
                        break;
                }

                if (!tailMoved) continue;

                if (!positionsVisited.TryAdd(tail, 1))
                {
                    positionsVisited[tail]++;
                }
            }
        }
    }

    public void Challenge2()
    {
        throw new NotImplementedException();
    }
}


//public class Day9 : IDay
//{
//    public enum Direction
//    {
//        Down = (Int32)'D',
//        Left = (Int32)'L',
//        Right = (Int32)'R',
//        Up = (Int32)'U',
//    }

//    private List<(Direction direction, Int32 move)> steps = new();

//    public async Task Setup(HttpClient client)
//    {
//        String input = await ExtraFunctions.MakeAdventOfCodeInputRequest(client, 9);

//        input = "R 4\nU 4\nL 3\nD 1\nR 4\nD 1\nL 5\nR 2\n";

//        steps = new List<(Direction, Int32)>(input.Length / 4);


//        foreach (String s in input.Split('\n', StringSplitOptions.RemoveEmptyEntries))
//        {
//            steps.Add((
//                    (Direction)s[0],
//                    Int32.Parse(s[2].ToString(CultureInfo.InvariantCulture)))
//                );
//        }

//    }

//    public void Challenge1()
//    {
//        Dictionary<Point, Int32> tailPositionsVisited = new()
//        {
//            {new Point(0,0), 1}
//        }; ;

//        Rope rope = new Rope(0, 0);
//        Rope tail = new Rope(0, 0);

//        foreach ((Direction direction, Int32 move) in steps)
//        {
//            for (Int32 i = 0; i < move; i++)
//            {
//                rope.Move(direction);
//                Point p = tail.FollowMove(direction, rope);
//                if(p == tail.PreviousPoint) continue;

//                PaintRopeAndTail(rope, tail);

//                if (!tail.HasMoved) continue;

//                if (tailPositionsVisited.ContainsKey(p))
//                {
//                    tailPositionsVisited[p]++;

//                }
//                else
//                {
//                    tailPositionsVisited.Add(p, 1);
//                }
//                tail.HasMoved = false;
//            }
//        }

//        var s = tailPositionsVisited.Count;

//        List<List<Boolean>> c = new List<List<Boolean>>().AddRange(new List<Boolean>[]);
//        foreach (KeyValuePair<Point, Int32> keyValuePair in tailPositionsVisited)
//        {
//            ;
//        }
//    }

//    public void Challenge2()
//    {
//        throw new NotImplementedException();
//    }

//    public void PaintRopeAndTail(Rope rope, Rope tail){



//         I know this is horribly inificient, but it's late and I want to go to bed.


//        Int32 xOffset = tail.CurrentPoint.X - rope.CurrentPoint.X;
//        Int32 yOffset = tail.CurrentPoint.Y - rope.CurrentPoint.Y;

//        List<List<Char>> sa = new()
//        {
//            new List<Char>(3){'x', 'x', 'x'},
//            new List<Char>(3){'x', 'S', 'x'},
//            new List<Char>(3){'x', 'x', 'x'},
//        };

//        sa[yOffset + 1][xOffset + 1] = 'T';

//        foreach (List<Char> chars in sa)
//        {
//            foreach (Char c in chars)
//            {
//                Console.Write(c);
//            }

//            Console.WriteLine();
//        }

//        Console.WriteLine("---------------------------------------------");

//    }

//     So turns out records are a thing
//    public record Point
//    {
//        public Int32 X;
//        public Int32 Y;

//        public Point(Int32 x, Int32 y)
//        {
//            X = x;
//            Y = y;
//        }



//        public static Boolean operator ==(Point a, Point b)
//        {
//            return a.X == b.X && a.Y == b.Y;
//        }
//        public static Boolean operator !=(Point a, Point b)
//        {
//            return !(a == b);
//        }

//        public override Boolean Equals(Object? obj)
//        {
//            return obj is Point other && Equals(other);
//        }

//        public Boolean Equals(Point other)
//        {
//            return X == other.X && Y == other.Y;
//        }

//        public override Int32 GetHashCode()
//        {
//            return HashCode.Combine(X, Y);
//        }

//        public override String ToString()
//        {
//            return $"X:{X}, Y:{Y}";
//        }

//        public Object Clone()
//        {
//            return new Point(X, Y);
//        }
//    }

//    public struct Rope
//    {
//        public Point PreviousPoint;
//        public Point CurrentPoint;
//        public Boolean HasMoved = false;

//        public Rope(Int32 x, Int32 y)
//        {
//            PreviousPoint = new Point(x, y);
//            CurrentPoint = new Point(x, y);
//        }


//        / <summary>
//        / Moves all directions indicated, and only after updates the previous Point
//        / </summary>
//        / <param name="directions"></param>
//        / <returns></returns>
//        public Point Move(Direction[] directions)
//        {
//            Point p = (Point) CurrentPoint.Clone();

//            foreach (Direction direction in directions)
//            {
//                Move(direction);
//            }

//            PreviousPoint = p;

//            return CurrentPoint;
//        }
//        public Point Move(Direction direction)
//        {
//            PreviousPoint = (Point) CurrentPoint;
//            switch (direction)
//            {
//                case Direction.Down:
//                    CurrentPoint.Y--;
//                    break;
//                case Direction.Left:
//                    CurrentPoint.X--;
//                    break;
//                case Direction.Right:
//                    CurrentPoint.X++;
//                    break;
//                case Direction.Up:
//                    CurrentPoint.Y++;
//                    break;
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
//            }

//            HasMoved = true;
//            return CurrentPoint;
//        }

//        public Point Move(Point point)
//        {
//            PreviousPoint = (Point)CurrentPoint.Clone();
//            CurrentPoint = (Point)point.Clone();
//            HasMoved = true;
//            return (Point)CurrentPoint.Clone();
//        }

//        public Point FollowMove(Direction direction, Rope toFollow)
//        {
//            if (CurrentPoint == toFollow.PreviousPoint) return CurrentPoint;

//            Int32 oppositeAxisMove = 0;

//            switch (direction)
//            {
//                case Direction.Down:

//                    if (toFollow.CurrentPoint.Y - CurrentPoint.Y != -2) return CurrentPoint;

//                    oppositeAxisMove = toFollow.CurrentPoint.X - CurrentPoint.X;
//                    switch (oppositeAxisMove)
//                    {
//                        case > 0:
//                            Move(new[] { Direction.Right, Direction.Down });
//                            break;
//                        case 0:
//                            Move(Direction.Down);
//                            break;
//                        case < 0:
//                            Move(new[] { Direction.Left, Direction.Down });
//                            break;
//                    }

//                    break;
//                case Direction.Up:
//                    if (toFollow.CurrentPoint.Y - CurrentPoint.Y != 2) return CurrentPoint;

//                    oppositeAxisMove = toFollow.CurrentPoint.X - CurrentPoint.X;
//                    switch (oppositeAxisMove)
//                    {
//                        case > 0:
//                            Move(new[] { Direction.Right, Direction.Up });
//                            break;
//                        case 0:
//                            Move(Direction.Up);
//                            break;
//                        case < 0:
//                            Move(new[] { Direction.Left, Direction.Up });
//                            break;
//                    }

//                    break;

//                case Direction.Left:
//                    if (toFollow.CurrentPoint.X - CurrentPoint.X != -2) return CurrentPoint;

//                    oppositeAxisMove = toFollow.CurrentPoint.Y - CurrentPoint.Y;
//                    switch (oppositeAxisMove)
//                    {
//                        case > 0:
//                            Move(new[] { Direction.Left, Direction.Up });
//                            break;
//                        case 0:
//                            Move(Direction.Left);
//                            break;
//                        case < 0:
//                            Move(new[] { Direction.Left, Direction.Down });
//                            break;
//                    }

//                    break;
//                case Direction.Right:
//                    if (toFollow.CurrentPoint.X - CurrentPoint.X != 2) return CurrentPoint;

//                    oppositeAxisMove = toFollow.CurrentPoint.Y - CurrentPoint.Y;
//                    switch (oppositeAxisMove)
//                    {
//                        case 1:
//                            Move(new[] { Direction.Right, Direction.Up });
//                            break;
//                        case 0:
//                            Move(Direction.Right);
//                            break;
//                        case -1:
//                            Move(new[] { Direction.Right, Direction.Down });
//                            break;
//                    }

//                    break;
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
//            }

//            HasMoved = true;
//            return CurrentPoint;
//        }
//    }
//}