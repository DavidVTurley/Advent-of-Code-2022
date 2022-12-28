using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;

namespace Advent_of_Code.Days;

public class Day10 : IDay
{
    public record Instruction(Command Command, Int32 Count);

    public enum Command
    {
        noop,
        addx
    };
    public Object Setup(HttpClient client)
    {
        String[] input = ExtraFunctions.MakeAdventOfCodeInputRequest(client, 10).Split('\n', StringSplitOptions.RemoveEmptyEntries);
        //input = $"addx 15\naddx -11\naddx 6\naddx -3\naddx 5\naddx -1\naddx -8\naddx 13\naddx 4\nnoop\naddx -1\naddx 5\naddx -1\naddx 5\naddx -1\naddx 5\naddx -1\naddx 5\naddx -1\naddx -35\naddx 1\naddx 24\naddx -19\naddx 1\naddx 16\naddx -11\nnoop\nnoop\naddx 21\naddx -15\nnoop\nnoop\naddx -3\naddx 9\naddx 1\naddx -3\naddx 8\naddx 1\naddx 5\nnoop\nnoop\nnoop\nnoop\nnoop\naddx -36\nnoop\naddx 1\naddx 7\nnoop\nnoop\nnoop\naddx 2\naddx 6\nnoop\nnoop\nnoop\nnoop\nnoop\naddx 1\nnoop\nnoop\naddx 7\naddx 1\nnoop\naddx -13\naddx 13\naddx 7\nnoop\naddx 1\naddx -33\nnoop\nnoop\nnoop\naddx 2\nnoop\nnoop\nnoop\naddx 8\nnoop\naddx -1\naddx 2\naddx 1\nnoop\naddx 17\naddx -9\naddx 1\naddx 1\naddx -3\naddx 11\nnoop\nnoop\naddx 1\nnoop\naddx 1\nnoop\nnoop\naddx -13\naddx -19\naddx 1\naddx 3\naddx 26\naddx -30\naddx 12\naddx -1\naddx 3\naddx 1\nnoop\nnoop\nnoop\naddx -9\naddx 18\naddx 1\naddx 2\nnoop\nnoop\naddx 9\nnoop\nnoop\nnoop\naddx -1\naddx 2\naddx -37\naddx 1\naddx 3\nnoop\naddx 15\naddx -21\naddx 22\naddx -6\naddx 1\nnoop\naddx 2\naddx 1\nnoop\naddx -10\nnoop\nnoop\naddx 20\naddx 1\naddx 2\naddx 2\naddx -6\naddx -11\nnoop\nnoop\nnoop".Split('\n', StringSplitOptions.RemoveEmptyEntries);

        foreach (String s in input)
        {
            Enum.TryParse(s.Substring(0, 4), out Command command);
            _instructions.Add(new Instruction(command, s.Length > 4 ? Int32.Parse(s.Substring(4)) : 0));
        }

        return _instructions;
    }

    private readonly List<Instruction> _instructions = new List<Instruction>();
    public void Challenge1(Object input)
    {
        Int32 sumOfSignalStrengths = 0;

        Int32 xRegister = 1;
        Int32 commandNumber = 0;

        Int32 instructionNumber = 0;
        
        for (Int32 cycle = 1; cycle < 240; cycle++)
        {
            
            Instruction instruction = _instructions[instructionNumber];
            sumOfSignalStrengths += GetSignalStrengthValueForCycle(cycle, xRegister);
            DrawPixel(xRegister, cycle);
            switch (instruction.Command)
            {
                case Command.noop:
                    break;
                case Command.addx:
                    cycle++;
                    DrawPixel(xRegister, cycle);
                    sumOfSignalStrengths += GetSignalStrengthValueForCycle(cycle, xRegister);
                    xRegister += instruction.Count;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            instructionNumber++;
        }
    }

    public void DrawPixel(Int32 xRegister, Int32 cycle)
    {
        cycle = cycle % 40;
        cycle -= 1;
        if (xRegister == cycle || xRegister + 1 == cycle || xRegister - 1 == cycle)
        {
            Console.Write('#');
        }
        else
        {
            Console.Write('.');
        }
        
        if (cycle % 40 == 0)
        {
            Console.WriteLine();
        }
    }

    public Int32 GetSignalStrengthValueForCycle(Int32 cycle, Int32 xRegister)
    {
        if ((cycle - 20) % 40 == 0 || cycle == 20)
        {
            Int32 x = cycle * xRegister;
            return x;
        }

        return 0;
    }

    public void Challenge2(Object input)
    {
        throw new NotImplementedException();
    }
}