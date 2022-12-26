using System.Text;

namespace Advent_of_Code.Days;

public class Day5 : IDay
{
    public async Task Setup(HttpClient client)
    {
        String input = await ExtraFunctions.MakeAdventOfCodeInputRequest(client, 5);
        //input = "    [D]    \n[N] [C]    \n[Z] [M] [P]\n 1   2   3 \n\nmove 1 from 2 to 1\nmove 3 from 1 to 3\nmove 2 from 2 to 1\nmove 1 from 1 to 2";
        _crates = new Crates(input);
        _crates2 = new Crates(input);
        input = input.Remove(0, input.IndexOf("\n\n", StringComparison.Ordinal)+2);
        String[] values = input.Split(' ', '\n');

        for (Int32 i = 1; i < values.Length; i+=6)
        {
            moves.Add(new(Int32.Parse(values[i]), Int32.Parse(values[i + 2]), Int32.Parse(values[i + 4])));
        }
    }

    private Crates _crates;
    private Crates _crates2;

    private List<(Int32 move, Int32 from, Int32 to)> moves = new();

    public void Challenge1()
    {
        foreach ((Int32 move, Int32 from, Int32 to) in moves)
        {
            _crates.MoveCratesOneByOne(from, move, to);
        }
        
        Console.WriteLine($"The top crates are: {_crates.TopCrates()}");
    }

    public void Challenge2()
    {
        foreach ((Int32 move, Int32 from, Int32 to) in moves)
        {
            _crates2.MoveCratesInMass(from, move, to);
        }

        Console.WriteLine($"The top crates are: {_crates2.TopCrates()}");
    }

    private class Crates
    {
        private List<Stack<Char>> _crates = new();
        
        public Crates(String inputFromAdventOfCode)
        {
            Int32 rowLength = inputFromAdventOfCode.IndexOf('\n') + 1;
            Int32 numOfColumns = rowLength / 4;
            Int32 columnHeight = inputFromAdventOfCode.IndexOf('1') / rowLength;

            for (Int32 columnIndex = 1; columnIndex < numOfColumns+1; columnIndex++)
            {
                Stack<Char> crateColumn = new();

                for (Int32 rowIndex = columnHeight-1; rowIndex > -1; rowIndex--)
                {
                    Int32 s = (columnIndex * 4 + (rowIndex * rowLength) - 3);

                    if (inputFromAdventOfCode[s] != ' ')
                    {
                        crateColumn.Push(inputFromAdventOfCode[s]);
                    }
                }

                _crates.Add(crateColumn);
            }
        }

        public void MoveCratesOneByOne(Int32 columnToMoveFrom, Int32 numOfCrates, Int32 targetColumn)
        {
            columnToMoveFrom -= 1;
            targetColumn -= 1;

            for (Int32 i = 0; i < numOfCrates; i++)
            {
                _crates[targetColumn].Push(_crates[columnToMoveFrom].Pop());
            }
        }

        public void MoveCratesInMass(Int32 columnToMoveFrom, Int32 numOfCrates, Int32 targetColumn)
        {
            columnToMoveFrom -= 1;
            targetColumn -= 1;

            Stack<Char> buffer = new();
            for (Int32 i = 0; i < numOfCrates; i++)
            {
                buffer.Push(_crates[columnToMoveFrom].Pop());
            }

            while (buffer.TryPop(out Char c))
            {
                _crates[targetColumn].Push(c);
            }
        }

        public String TopCrates()
        {
            StringBuilder sb = new StringBuilder(_crates.Count);
            foreach (Stack<Char> crate in _crates)
            {
                sb.Append(crate.Peek());
            }

            return sb.ToString();
        }
    }
}