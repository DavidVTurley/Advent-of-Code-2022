using System.Runtime.CompilerServices;

namespace Advent_of_Code.Days;

public class Day3 : IDay
{
    public async Task Setup(HttpClient client)
    {
        String input = await ExtraFunctions.MakeAdventOfCodeInputRequest(client, 3);
        //input = "vJrwpWtwJgWrhcsFMMfFFhFp\njqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL\nPmmdzqPrVvPwwTWBwg\nwMqvLMZHhHMvwLHjbvcjnnSBnvTQFn\nttgJtRGJQctTZtZT\nCrZsJsPPZsGzwwsLwLmpwMDw";
        _bags = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList();

        foreach (String bag in _bags)
        {
            Int32 splitIndex = (bag.Length / 2);
            _compartments.Add((bag.Substring(0, splitIndex), bag.Substring(splitIndex)));

            if (_compartments[_compartments.Count - 1].compartment1.Length != _compartments[_compartments.Count - 1].compartment2.Length)
            {
                throw new Exception();
            }
        }
    }

    private List<String> _bags = new();
    private readonly List<(String compartment1, String compartment2)> _compartments = new();
    public void Challenge1()
    {
        Int32 count = 0;
        foreach ((String? compartment1, String? compartment2) in _compartments)
        {
            foreach (Char c in compartment2)
            {
                if (!compartment1.Contains(c)) continue;
                count += GetPriority(c);
                break;
            }

        }

        Console.WriteLine($"The priority of the items in the compartments is: {count}");
    }

    private Int32 GetPriority(Char c)
    {
        if (Char.IsLower(c)) //Lowercase a = 97 value = 1

        {
            return c - 96;
        }
        else                 //Uppercase A = 65 value = 27
        {
            return c - 38;
        }
    }

    public void Challenge2()
    {
        Int32 count = 0;

        for (Int32 i = 0; i < _bags.Count; i+=3)
        {
            // I know that this creates duplicate strings
            // TODO upgrade so no duplication of strings happens

            String bag1 = _bags[i];
            String bag2 = _bags[i+1];
            String bag3 = _bags[i+2];

            foreach (Char c in bag1)
            {
                if (!bag2.Contains(c) || !bag3.Contains(c)) continue;

                count += GetPriority(c);
                break;
            }
        }
    }
}